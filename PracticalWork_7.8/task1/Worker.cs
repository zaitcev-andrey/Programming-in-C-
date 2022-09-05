using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task1
{
    internal struct Worker
    {
        #region Поля

        private int id;
        private DateTime note_date;
        private string fio;
        private uint age;
        private uint height;
        private DateTime birth_date;
        private string birth_place;

        #endregion

        #region Конструкторы
        /// <summary>
        /// Конструктор для всех параметров
        /// </summary>
        /// <param name="id">Идентификатор работника</param>
        /// <param name="note_date">Дата записи</param>
        /// <param name="fio">Ф.И.О</param>
        /// <param name="age">Возраст</param>
        /// <param name="height">Рост</param>
        /// <param name="birth">Дата рождения</param>
        /// <param name="birth_place">Место рождения</param>
        public Worker(int id, DateTime note_date, string fio, uint age,
            uint height, DateTime birth_date, string birth_place)
        {
            this.id = id;
            this.note_date = note_date;
            this.fio = fio;
            this.age = age;
            this.height = height;
            this.birth_date = birth_date;
            this.birth_place = birth_place;
        }

        #endregion

        #region Свойства

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public DateTime Note_date
        {
            get { return note_date; }
            set { note_date = value; }
        }

        public string Fio
        {
            get { return fio; }
            set { fio = value; }
        }

        public int Age 
        {
            get { return Convert.ToInt32(age); }
            set { age = Convert.ToUInt32(value); } 
        }

        public int Height
        {
            get { return Convert.ToInt32(height); }
            set { height = Convert.ToUInt32(value); }
        }

        public DateTime Birth_date
        {
            get { return birth_date; }
            set { birth_date = value; }
        }

        public string Birth_place
        {
            get { return birth_place; }
            set { birth_place = value; }
        }

        #endregion

        #region Методы
        /// <summary>
        /// Распечатываем данные работника
        /// </summary>
        public void Print()
        {
            Console.WriteLine($"id: {Id}\tдата записи: {Note_date}\t" +
                $"Ф.И.О: {Fio}\tВозраст: {Age}\tРост: {Height}\t" +
                $"Дата рождения: {Birth_date}\tМесто рождения: {Birth_place}");
        }
        #endregion
    }
}
