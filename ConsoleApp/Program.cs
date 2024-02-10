using DataBase;
using DataBase.Command;
using DataBase.Entity;

CommandUser commandUser = new CommandUser();
CommandPeople commandPeople = new CommandPeople();

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
        commandPeople.CreatePeople();
        Console.WriteLine("Люди были добавлены в базу");
        break;
    case "4":
        var  array = commandPeople.OutputPeople();
        // Выводим весь список людей
        foreach (var person in array)
        {
            Console.WriteLine($"Id: {person.Id}, Name: {person.Name}, Age: {person.Age}");
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
        User user = new User();
        user.Name = text;
        commandUser.AddUser(user);
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
        var exist = commandUser.IsExistUser(text);
        if(exist)
        Console.WriteLine($"Пользователь {text} есть в бд");
        else
            Console.WriteLine($"Пользователя {text} нет в бд");
    }
    else
        Console.WriteLine("Вы ввели не корректные данные.");
}