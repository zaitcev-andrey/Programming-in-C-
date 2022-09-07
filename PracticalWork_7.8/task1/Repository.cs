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
        /// Максимальный индекс записи для уникальности идентификаторов
        /// </summary>
        private int max_index;
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
            
            index = 0;
            workers = new Worker[2];
            Update();
            if(index == 0)
                max_index = 0;
            else
                max_index = workers[index-1].Id;
        }

        #endregion

        #region Методы
        #region Методы на обновление и получение записей
        /// <summary>
        /// Перевыделение памяти для массива работников
        /// </summary>
        /// <param name="flag">Перевыделяем память, если истина</param>
        private void Resize(bool flag)
        {
            if (flag)
            {
                Array.Resize(ref workers, workers.Length * 2);
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
            // Создаём новый массив без пустых записей в конце из-за capacity
            Worker[] copy_workers = new Worker[index];
            Array.Copy(workers, copy_workers, index);
            return copy_workers;
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
            if (0 <= id && id < index)
                worker = workers[id];

            return worker;
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
            // Создаём новый массив без пустых записей в конце из-за capacity
            Worker[] copy_workers = new Worker[index];
            Array.Copy(workers, copy_workers, index);

            Worker[] sorted = copy_workers.OrderBy(w => w.Note_date).ToArray();

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
            for (int i = 0; i < index; i++)
            {
                workers[i].Print();
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
            Worker[] sorted;
            // Создаём новый массив без пустых записей в конце из-за capacity
            Worker[] copy_workers = new Worker[index];
            Array.Copy(workers, copy_workers, index);
            // В switch 7 условий, так как в Worker всего 7 полей
            switch (field_index)
            {
                case 0:
                    sorted = copy_workers.OrderBy(w => w.Id).ToArray();
                    break;
                case 1:
                    sorted = copy_workers.OrderBy(w => w.Note_date).ToArray();
                    break;
                case 2:
                    sorted = copy_workers.OrderBy(w => w.Fio).ToArray();
                    break;
                case 3:
                    sorted = copy_workers.OrderBy(w => w.Age).ToArray();
                    break;
                case 4:
                    sorted = copy_workers.OrderBy(w => w.Height).ToArray();
                    break;
                case 5:
                    sorted = copy_workers.OrderBy(w => w.Birth_date).ToArray();
                    break;
                case 6:
                    sorted = copy_workers.OrderBy(w => w.Birth_place).ToArray();
                    break;
                default:
                    sorted = copy_workers;
                    break;
            }
            return sorted;
        }

        private string GetStringFromWorker(Worker w)
        {
            return $"{w.Id}#{w.Note_date}#" +
                    $"{w.Fio}#{w.Age}#" +
                    $"{w.Height}#{w.Birth_date}#" +
                    $"{w.Birth_place}";
        }
        #endregion

        #region Методы на добавление и удаление записей

        /// <summary>
        /// Добавление работника в файл
        /// и в собственный массив работников
        /// </summary>
        /// <param name="worker">Экземпляр работника</param>
        public void AddWorker(Worker worker)
        {
            Worker new_worker = new Worker(max_index, worker.Note_date, worker.Fio,
                worker.Age, worker.Height, worker.Birth_date, worker.Birth_place);
            Resize(index >= workers.Length);
            workers[index] = new_worker;
            index++;
            max_index++;
            using (StreamWriter sw = new StreamWriter(path, true))
            {
                sw.WriteLine(GetStringFromWorker(new_worker));
            }
        }

        /// <summary>
        /// Удаление работника из файла
        /// и из собственного массива работников
        /// по указанному id
        /// </summary>
        /// <param name="id">id работника</param>
        public void DeleteWorker(int id)
        {
            if (0 <= id && id < max_index)
            {
                // Более медленный вариант
                //workers = workers.Where(x => x.Id != id).ToArray();

                if (id == max_index)
                    max_index--;
                // Удаляем работника по индексу и сдвигаем влево, чтобы избежать пустоты
                int from = Array.FindIndex(workers, worker => worker.Id == id);
                if (from == -1)
                    return;
                Array.Clear(workers, from, 1);
                for (int i = from; i < index - 1; i++)
                    workers[i] = workers[i + 1];
                index--;

                // без true, чтобы перезаписать файл
                using (StreamWriter sw = new StreamWriter(path))
                {
                    for (int i = 0; i < index; i++)
                        sw.WriteLine(GetStringFromWorker(workers[i]));
                }
            }
        }

        #endregion

        #endregion
    }
}
