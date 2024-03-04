﻿using Common.Standard.Interfaces.Model;
using System.Collections.ObjectModel;
using System.Linq.Expressions;

namespace Authorized
{
    public class InitProductRepository : IReadOnlyRepository<IProduct>
    {
        private readonly ReadOnlyObservableList<IProduct> products;
        private readonly ObservableCollection<IProduct> productslist = new();

        public InitProductRepository()
        {
            products = new(productslist);
        }

        public bool Any(Expression<Func<IProduct, bool>> expression)
        {
            return Queryable.AsQueryable(products.Cast<IProduct>()).Any(expression);
        }

        public IProduct? FirstOrDefault()
        {
            if (products.Count == 0)
                return null;
            return products[0];
        }

        public IProduct? FirstOrDefault(Expression<Func<IProduct, bool>> expression)
        {
            return Queryable.AsQueryable(products.Cast<IProduct>()).FirstOrDefault(expression);
        }

        public void Load()
        {
            productslist.Clear();
            var products = Enumerable.Range(1, 10).Select(i => new ProductDto
            {
                Id = i,
                Name = digitalProducts[Random.Shared.Next(0, digitalProducts.Length - 1)],
                Description = digitalDescriptions[Random.Shared.Next(0, digitalDescriptions.Length - 1)],
            }).ToArray();
            foreach (var product in products)
            {
                productslist.Add(product);
            }
        }
        private static readonly string[] digitalProducts = new string[]  {
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

        private static readonly string[] digitalDescriptions = new string[] {
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
        public IReadOnlyObservableCollection<IProduct> ToObservableCollections()
        {
            return products;
        }

        public IQueryable<IProduct> Where(Expression<Func<IProduct, bool>> expression)
        {
            return Queryable.AsQueryable(products.Cast<IProduct>()).Where(expression);
        }
    }
}
