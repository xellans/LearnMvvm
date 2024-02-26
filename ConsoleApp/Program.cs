using DataBase;
using Microsoft.EntityFrameworkCore;
using Repositories;

Command<User> user = new Command<User>();

Command<People> people = new Command<People>();

people.FirstOrDefault(x => x.Name == "");
var ss = people.Any(x => x.CompletedTasks == 1);
var re = people.Where(x => x.Name == x.Name).ToList();

Command<Product> product = new Command<Product>();
var res = product.Where(x => x.Name == x.Name).ToList();
var ssss = product.Any(x => x.Description == "");


string info = @"1 - Добавить нового пользователя
2 - Проверить есть ли пользователь в бд
3 - Сгенирировать в базу данных людей
4- Вывести список людей";
Console.WriteLine(info);

    switch (Console.ReadLine())
    {
        case "1":
        Console.WriteLine("Введите имя пользователя для добавления в бд");
        AddUser();
        break;
        case "2":
        Console.WriteLine("Введите имя пользователя для проверки");
        ExistUser();
        break;
    case "3":
        people.CreatePeople();
        Console.WriteLine("Люди были добавлены в базу");
        break;
    case "4":
        var array = people.GetPeopleCollection();
        // Выводим весь список людей
        foreach (var person in array)
        {
            Console.WriteLine($"Id: {person.Id}, Name: {person.Name}, CompletedTasks: {person.CompletedTasks}, RemainsExecute: {person.RemainsExecute}");
        }
        break;
}

Console.WriteLine("Работа окончена");

//Добавить нового пользователя в бд
void AddUser()
{
    string text = Console.ReadLine();
    if (text != null && text != "")
    {
        User _user = new User();
        _user.Name = text;
        if(user.Any(x => x.Name == _user.Name));
        user.Add(_user);
        Console.WriteLine($"Пользователь {text} был добавлен");
    }
    else
        Console.WriteLine("Вы ввели не корректные данные.");
}
///Проверить если пользователь в бд
void ExistUser()
{
    string text = Console.ReadLine();
    if (text != null && text != "")
    {
        var _user = new User();
        _user.Name = text;
        var exist = user.Any(x => x.Name == _user.Name);
        if(exist)
        Console.WriteLine($"Пользователь {text} есть в бд");
        else
            Console.WriteLine($"Пользователя {text} нет в бд");
    }
    else
        Console.WriteLine("Вы ввели не корректные данные.");
}