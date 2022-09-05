using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task1
{
    internal class Repository
    {
        #region Поля

        /// <summary>
        /// Путь к файлу
        /// </summary>
        private string path;
        /// <summary>
        /// Индексация работников
        /// </summary>
        private int index;
        /// <summary>
        /// Массив работников
        /// </summary>
        private Worker[] workers;

        #endregion

        #region Конструкторы
        /// <summary>
        /// Конструктор, в котором устанавливается связь с файлом
        /// и сразу загружаются данные
        /// </summary>
        /// <param name="path">Путь к файлу</param>
        public Repository(string path)
        {
            this.path = path;
            if(!File.Exists(path))
            {
                // обязательно создаём файл через поток,
                // иначе без него файл потом не закрыть,
                // что приведёт к ошибке
                FileStream fs = File.Create(path);
                fs.Close();
            }
            
            this.index = 0;
            workers = new Worker[2];
            Update();
        }

        #endregion


        #region Методы
        /// <summary>
        /// Перевыделение памяти для массива работников
        /// </summary>
        /// <param name="flag">Перевыделяем память, если истина</param>
        private void Resize(bool flag)
        {
            if(flag)
            {
                Array.Resize(ref this.workers, this.workers.Length * 2);
            }
        }

        /// <summary>
        /// Загрузка данных из файла, если массив работников пуст
        /// </summary>
        public void Update()
        {
            if (index == 0)
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    while (!sr.EndOfStream)
                    {
                        string[] args = sr.ReadLine().Split('#');
                        Resize(index >= workers.Length);
                        workers[index] = new Worker(Convert.ToInt32(args[0]),
                            Convert.ToDateTime(args[1]), args[2], Convert.ToUInt32(args[3]),
                            Convert.ToUInt32(args[4]), Convert.ToDateTime(args[5]), args[6]);
                        index++;
                    }
                }
            }
        }

        /// <summary>
        /// Возвращаем массив работников, 
        /// он уже будет синхронизован с данными из файла
        /// </summary>
        /// <returns>Массив работников</returns>
        public Worker[] GetAllWorkers()
        {
            return workers;
        }

        /// <summary>
        /// Поиск в файле работника по указанному id
        /// и его возврат
        /// </summary>
        /// <param name="id">id работника</param>
        /// <returns>Экземпляр работника</returns>
        public Worker GetWorkerById(int id)
        {
            Worker worker = new Worker();
            // Проверяем id на корректность
            if(0 <= id  && id < index)
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    bool flag = true;

                    while (!sr.EndOfStream && flag)
                    {
                        string[] args = sr.ReadLine().Split('#');
                        if (Convert.ToInt32(args[0]) == id)
                        {
                            worker.Id = id;
                            worker.Note_date = Convert.ToDateTime(args[1]);
                            worker.Fio = args[2];
                            worker.Age = Convert.ToInt32(args[3]);
                            worker.Height = Convert.ToInt32(args[4]);
                            worker.Birth_date = Convert.ToDateTime(args[5]);
                            worker.Birth_place = args[6];
                            flag = false;
                        }
                    }
                }
            }
            
            return worker;
        }

        /// <summary>
        /// Удаление работника из файла
        /// и из собственного массива работников
        /// по указанному id
        /// </summary>
        /// <param name="id">id работника</param>
        public void DeleteWorker(int id)
        {
            if(0 <= id && id < index)
            {
                //Array.Clear(workers, id, 1);
                workers = workers.Where(x => x.Id != id).ToArray();
                index--;
                // Обновляем порядок индексов с удалённого элемента
                // и до конца массива
                for (int i = id; i < index; i++)
                    workers[i].Id = i;

                bool flag = true;

                // Создаём на диске временный пустой файл
                string tempFile = Path.GetTempFileName();
                using (StreamReader sr = new StreamReader(path))
                using (StreamWriter sw = new StreamWriter(tempFile))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] args = line.Split('#');
                        if (flag)
                            if (Convert.ToInt32(args[0]) != id)
                                sw.WriteLine(line);
                            else
                                flag = false;

                        // обновляем нумерацию индексов
                        else
                        {
                            int last_ind = line.IndexOf('#');
                            string substr = line.Substring(0, last_ind);
                            int new_ind = Convert.ToInt32(args[0]) - 1;
                            line = line.Replace(substr, Convert.ToString(new_ind));
                            sw.WriteLine(line);
                        }
                    }
                }
                // Теперь удаляем старый файл и перемещаем
                // новый файл с обновлёнными данными на место старого
                File.Delete(path);
                File.Move(tempFile, path);
            }
        }

        /// <summary>
        /// Добавление работника в файл
        /// и в собственный массив работников
        /// </summary>
        /// <param name="worker">Экземпляр работника</param>
        public void AddWorker(Worker worker)
        {
            worker.Id = this.index;
            Resize(this.index >= workers.Length);
            workers[index] = worker;
            index++;
            using(StreamWriter sw = new StreamWriter(path, true))
            {
                string result = $"{worker.Id}#{worker.Note_date}#{worker.Fio}#{worker.Age}#" +
                    $"{worker.Height}#{worker.Birth_date}#{worker.Birth_place}";
                
                sw.WriteLine(result);
            }
        }

        /// <summary>
        /// Возврат работников в указанном диапазоне
        /// дат добавления записей после их сортировки
        /// </summary>
        /// <param name="dateFrom">Нижняя граница диапазона</param>
        /// <param name="dateTo">Верхняя граница диапазона</param>
        /// <returns>Массив работников</returns>
        public Worker[] GetWorkersBetweenTwoDates(DateTime dateFrom, DateTime dateTo)
        {
            Worker[] sorted = workers.OrderBy(w => w.Note_date).ToArray();

            int fromIndex = 0;
            while (true && fromIndex < index)
            {
                if (sorted[fromIndex].Note_date >= dateFrom)
                    break;
                fromIndex++;
            }
            
            int lastIndex = fromIndex;
            while (true && lastIndex < index)
            {
                if (sorted[lastIndex].Note_date > dateTo)
                    break;
                lastIndex++;
            }
            Worker[] result = new Worker[lastIndex - fromIndex];
            Array.Copy(sorted, fromIndex, result, 0, result.Length);

            return result;
        }

        /// <summary>
        /// Распечатываем данные всех работников
        /// </summary>
        public void Print()
        {
            foreach (Worker worker in workers)
            {
                worker.Print();
            }
        }

        /// <summary>
        /// Получаем массив работников, отсортированных по указанному полю.
        /// Но в случае неверно указанного поля возвращается исходный массив
        /// </summary>
        /// <param name="field_index">Индекс поля, по которому сортируем</param>
        /// <returns>Массив работников</returns>
        public Worker[] OrderByField(int field_index)
        {
            // В switch 7 условий, так как в Worker всего 7 полей
            Worker[] sorted;
            switch (field_index)
            {
                case 0:
                    sorted = workers.OrderBy(w => w.Id).ToArray();
                    break;
                case 1:
                    sorted = workers.OrderBy(w => w.Note_date).ToArray();
                    break;
                case 2:
                    sorted = workers.OrderBy(w => w.Fio).ToArray();
                    break;
                case 3:
                    sorted = workers.OrderBy(w => w.Age).ToArray();
                    break;
                case 4:
                    sorted = workers.OrderBy(w => w.Height).ToArray();
                    break;
                case 5:
                    sorted = workers.OrderBy(w => w.Birth_date).ToArray();
                    break;
                case 6:
                    sorted = workers.OrderBy(w => w.Birth_place).ToArray();
                    break;
                default:
                    sorted = workers;
                    break;
            }
            return sorted;
        }

        #endregion

    }
}
