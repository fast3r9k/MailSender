# MailSender
1. Выполнить без использования среды разработки, используя только ручку и лист бумаги, задачу на написание консольного приложения нахождения факториала числа N.
int a = 5;
int res = 1;
for(int i = 1; i <= a; i++)
  res *= i;
 Console.Writeline(res);
 ----------------------------------
2. Есть ли проблемы в следующем коде?
int i = 1;
object obj = i;
++i;
Console.WriteLine(i);
Console.WriteLine(obj);
Console.WriteLine((short)obj);

Последняя строка не скомпилируется
 ----------------------------------
 
3. Есть таблица Users. Столбцы в ней — Id, Name. Написать SQL-запрос, который выведет имена пользователей, начинающиеся на 'A' и встречающиеся в таблице более одного раза, и их количество.
Select Name, COUNT(Name) from Users GROUP BY Name HAVING COUNT(name) > 1 (Не проверял пока работоспособность)
 ----------------------------------
 
4. Каков результат вывода следующего кода?
private enum SomeEnum
{
    First = 4,
    Second,
    Third = 7
}
static void Main(string[] args)
{
    Console.WriteLine((int)SomeEnum.Second);
}

5 , т.к тип enum 
