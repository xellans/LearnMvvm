using DataBase;
using Microsoft.EntityFrameworkCore;
using Repositories.Inerfaces;
using System;
using System.Collections.ObjectModel;
using System.Xml.Linq;

namespace Repositories
{
    public class Command<T> : IRepository<T>
    {
        public Context Context; //*Сделал публичным так как реализованы не все функции ef, сейчас они не нужны для реализации. Но можно сделать позже.
                                //Потом сделаю это свойство приватным*//
        public Command()
        {
            Context = new Context();
            Context.Database.EnsureCreated();
            Context.People.Load();
            Context.Product.Load();
        }
        /// <summary>
        /// Список людей
        /// </summary>
        /// <returns></returns>
        public ReadOnlyObservableCollection<People> GetPeopleCollection() => new ReadOnlyObservableCollection<People>(Context.People.Local.ToObservableCollection());


        /// <summary>
        /// Список товаров
        /// </summary>
        /// <returns></returns>
        public ReadOnlyObservableCollection<Product> GetProductCollection() => new ReadOnlyObservableCollection<Product>(Context.Product.Local.ToObservableCollection());
       
    

        /// <summary>
        /// Проверка на наличие в бд.
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public bool IsExist(T? t)
        {
            bool result = false;
            if (t == null)
                throw new ArgumentNullException($"{t?.GetType().Name}, значение объекта равно null");
            if (t is User user)
                result = Context.User.Any(x => x.Name == user.Name);

            if (t is People people)
                result = Context.People.Any(x => x.Name == people.Name);

            if (t is Product product)
                result = Context.Product.Any(x => x.Name == product.Name);
            return result;
        }

        /// <summary>
        /// Добавить новый элемент в бд
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public T? Add(T t)
        {
            try
            {
                Context.Add(t);
                Context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
            return t;
        }

        public T Clone(T t) => t;

        /// <summary>
        /// Удаление объекта из бд
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public T? Remove(T t)
        {
            if (t == null)
                throw new ArgumentNullException($"Ошибка при удалении {t?.GetType().Name}, значение объекта равно null");
            Context.Remove(t);
            Context.SaveChanges();
            return t;
        }


        #region Заполнение бд, нужно только для примера.
        public void CreatePeople()
        {
            if (Context.People.ToList().Count > 0)
                return;
            string[] peopleArray = { "Алиса", "Екатерина", "Василий", "Андрей", "Пётр", "Инна", "Вика", "Жанна", "Ксюша", "Анатолий" };
            for (int i = 0; i < 200; i++)
            {
                People people = new People()
                {
                    Name = peopleArray[Random.Shared.Next(0, 9)],
                    CompletedTasks = Random.Shared.Next(10, 1000),
                    RemainsExecute = Random.Shared.Next(10, 1000)
                };
                Context.People.Add(people);
            }
            Context.SaveChanges();
        }
        public void CreateProduct()
        {
            if (Context.Product.ToList().Count > 0)
                return;
            for (int i = 0; i < 200; i++)
            {
                Product product = new Product()
                {
                    Name = digitalProducts[Random.Shared.Next(0, digitalProducts.Length - 1)],
                    Description = digitalDescriptions[Random.Shared.Next(0, digitalDescriptions.Length - 1)]
                };
                Context.Product.Add(product);
            }
            Context.SaveChanges();
        }
        string[] digitalProducts = new string[]  {
    "Смартфон", "Ноутбук", "Планшет", "Смарт-часы", "Наушники", "Фотоаппарат", "Игровая консоль", "Внешний жесткий диск",
    "Роутер", "Флешка", "Принтер", "Монитор", "Клавиатура", "Мышь", "Веб-камера", "Компьютерная мышь",
    "Жесткий диск", "Процессор", "Видеокарта", "Материнская плата", "Оперативная память", "SSD", "ТВ-приставка", "Колонки",
    "Аккумулятор", "Зарядное устройство", "Клавиатура для планшета", "Стилус", "Игровая мышь", "Микрофон", "Компьютерные колонки",
    "Сетевой фильтр", "Акустическая система", "ЖК-монитор", "Презентер", "Системный блок", "CD/DVD-привод", "Мультимедийная клавиатура", "Геймпад",
    "ЖК-телевизор", "Беспроводные наушники", "Сетевой адаптер", "VR-очки", "Генератор паролей", "Смарт-плеер", "Медиацентр", "Цифровая зеркальная камера",
    "Видеорегистратор", "Портативная зарядка", "Игровая гарнитура", "ТВ-тюнер", "Носимое устройство", "Ультрабук", "Тачпад", "Интернет-телефон",
    "Мини-планшет", "Устройство беспроводного доступа", "Виртуальная клавиатура", "Цифровой фоторамка", "Сетевой накопитель", "Искусственный интеллект", "Смарт-пульт", "Дрон",
    "Ретро-консоль", "Домашний кинотеатр", "Проекционное устройство", "Портативный DVD-плеер", "Умный дом", "Умные акустические системы", "Смарт-гироскутер", "Смарт-термометр",
    "Смарт-стерео", "Смарт-весы", "Смарт-телескоп", "Смарт-фотоаппарат", "Смарт-гарнитура", "Смарт-гаджет", "Смарт-игра", "Смарт-трекер",
    "Смарт-робот", "Смарт-устройство", "Смарт-сканер", "Смарт-моноблок", "Смарт-монопод", "Смарт-подвес", "Смарт-домофон", "Смарт-календарь",
    "Смарт-карман", "Смарт-бинокль", "Смарт-очки", "Смарт-органайзер", "Смарт-авто", "Смарт-ретро", "Смарт-штаны", "Смарт-симулятор"
};

        string[] digitalDescriptions = new string[] {
    "Мощное мобильное устройство", "Портативный компьютер для работы и развлечений",
    "Удобный планшет для мультимедийных контента", "Интеллектуальные часы с множеством функций",
    "Качественное звучание и комфорт использования", "Устройство для фотографии, видеосъемки",
    "Платформа для игр и развлечений", "Дополнительное хранилище для данных",
    "Устройство для подключения к интернету", "Носитель данных и файлов",
    "Устройство для печати документов", "Дисплей для отображения информации",
    "Устройство для ввода текста и команд", "Устройство для управления курсором",
    "Видеосвязь и онлайн-общение", "Удобное устройство для переноски информации",
    "Хранилище для данных и программного обеспечения", "Центр обработки данных и команд",
    "Основной узел обработки графики", "Основная часть компьютера",
    "Модуль оперативной памяти", "Накопитель для быстрого доступа к данным",
    "Устройство для просмотра интернет-телевидения", "Звуковое сопровождение мультимедиа-контента",
    "Источник питания для устройств", "Устройство для зарядки аккумуляторов",
    "Клавиатура с интеграцией планшета", "Ручка для работы с сенсорными устройствами",
    "Устройство для игровой активности", "Запись звука и общения",
};
        #endregion
    }
}
