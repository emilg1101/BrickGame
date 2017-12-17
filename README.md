BrickGameEmulator
=================

Эмулятор игровой платформы BrickGame.

Разработчики
------------

1. ---
2. ---
3. ---

Документация (Кратко)
---------------------

### Class BGField

Класс для работы с игровым полем. Представоляет собой двумерный массив 10 на 20.

Кострукторы:
1.    **BGField()** - Создает двумерный массив 10 на 20 состоящий из нулей.
2.    **BGField(int[][] field)** - Принимает двумерный массив и с ним работает.

Доступные методы:
1.    **int GetWidth()** - Возвращает ширину поля.
2.    **int GetHeight()** - Возвращает высоту поля.
3.    **int GetValueByPosition(int x, int y)** - Возвращает значение ячейки поля в указанных координатах.
4.    **void SetValueAtPosition(int x, int y, int value)** - Устанавливает указанное значение в ячейку поля в указанных координатах.

### Class Game

Методы:
1.    **void Create()** - Вызывается кода игра была впервые создана. Здесь вы должны выполнить всю статическую настройку: инициализация игрового поля.
2.    **BGField Run(ConsoleKey key)** - Вызывается при каждом игровом такте.
3.    **string SplashScreen()** - Вызывается для установки превью-анимации игры.
4.    **void Start()** - Срабатывает при выходе из состояния PAUSE
5.    **void Pause()** - Срабатывает при входе в состояние PAUSE
6.    **void Destroy(BGDataStorage storage)** - Вызывается при смене игры.
7.    **void SetScore(int score)** - Устанавливает количество очков.
8.    **void SetSpeed(int speed)** - Устанавливает скорость игры. Значение от 1 до 15.
9.    **void SetLevel(int level)** - Устанавливает уровень игры.
10.    **bool IsPause()** - Возвращает состояние игры.   

### Пример SampleGame

1. Наследуйте класс **Game** в вашей игре.

```C#
public class SampleGame : Game
{
    //implements
}
```

2. Перепешите методы **void Сreate()**, **BGField Run(ConsoleKey)**, **string SplashScreen()**, **void Start()**, **void Pause()**, **void Destroy(BGDataStorage)**.

```C#
public class SampleGame : Game
{
    public override void Create()
    {
        base.Create();
    }

    public override BGField Run(ConsoleKey key)
    {
        return base.Run(key);
    }

    public override string SplashScreen()
    {
        return base.SplashScreen();
    }

    public override void Start()
    {
        base.Start();
    }

    public override void Pause()
    {
        base.Pause();
    }

    public override void Destroy(BGDataStorage storage)
    {
        base.Destroy(storage);
    }
}
```

3. Теперь надо создать игровое поле (BGField) в методе **Create()**

```C#
public class SampleGame : Game
{
    private BGField field;

    public override void Create()
    {
        field = new BGField();
    }
    
    //...
    
 }
 ```

4. Реализуем метод Run()

```C#
public class SampleGame : Game
{
    private BGField field;
    
    //...

    public override BGField Run(ConsoleKey key)
    {
        return field;
    }
    
    //...
    
 }
 ```
 
 5. Устанавливаем превью-анимацию [Пример](https://github.com/emilg1101/BrickGame/blob/master/bin/Debug/tanki.sph)
 
 
```C#
public class SampleGame : Game
{
   
    //...

    public override string SplashScreen()
    {
        return "sample_game.sph"; //Файл с анимацией
    }
    
    //...
    
 }
 ```
 
 6. Добавляем обработку нажатия клавиши.
 
  
```C#
public class SampleGame : Game
{
   
    //...
    
    public override BGField Run(ConsoleKey key)
    {   
        if (key == ConsoleKey.UpArrow) Up();
        if (key == ConsoleKey.DownArrow) Down();
        if (key == ConsoleKey.RightArrow) Right();
        if (key == ConsoleKey.LeftArrow) Left();
        return field;
    }
    
    public void Up(){}
    
    public void Down(){}
    
    public void Right(){}
    
    public void Left(){}
    
    //...
}
```

 7. Добавляем передвижения.
 
 ```C#
 public class SampleGame : Game
 {
    //Первоначальная позиция точки
    private int x = 0;
    private int y = 0;
    
     //...
     
     private void Up()
     {
        field.SetValueAtPosition(x, y, 0); //Закрашиваем точку в текущей позиции 
        if (y != 0) y--; //Меняем позицию
        field.SetValueAtPosition(x, y, 1); //Рисуем точку в новой позиции
     }
     
     private void Down(){}
     
     private void Right(){}
     
     private void Left(){}
     
     //...
 }
 ```

 Полный код
 ```C#
 namespace BrickGameEmulator
{
    public class SampleGame : Game
    {
        private BGField field;

        private int x = 0;
        private int y = 0;
        
        public override void Create()
        {
            field = new BGField();
        }

        public override BGField Run(ConsoleKey key)
        {
            if (key == ConsoleKey.UpArrow) Up();
            if (key == ConsoleKey.DownArrow) Down();
            if (key == ConsoleKey.RightArrow) Right();
            if (key == ConsoleKey.LeftArrow) Left();
            return field;
        }
        
        private void Up()
        {
            field.SetValueAtPosition(x, y, 0);
            if (y != 0) y--;
            field.SetValueAtPosition(x, y, 1);
        }

        private void Down()
        {
            field.SetValueAtPosition(x, y, 0);
            if (y != field.GetHeight() - 1) y++;
            field.SetValueAtPosition(x, y, 1);
        }
        
        private void Right()
        {
            field.SetValueAtPosition(x, y, 0);
            if (x != field.GetWidth() - 1) x++;
            field.SetValueAtPosition(x, y, 1);
        }
        
        private void Left()
        {
            field.SetValueAtPosition(x, y, 0);
            if (x != 0) x--;
            field.SetValueAtPosition(x, y, 1);
        }

        public override string SplashScreen()
        {
            return "sample_game.sph";
        }
    }
}
```

### Добавление игры в эмулятор

1. Открыть BrickGame.cs 
2. Добавить в массив экземпляр класса игры
```C#
games = new Game[]
{
    //...
    new SampleGame(),
    //...
};
```
