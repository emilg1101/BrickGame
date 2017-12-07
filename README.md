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

### Class BGSurface

Класс для взаимодействия с консолью.

Сеттеры/Геттеры:
1.    **int Score** - Текущее кол-во очков игрока.
2.    **int Level** - Текущий уровень. (Не работает пока)
3.    **int Speed** - Текущая скорость игры. (Не работает пока)

Доступные методы:
1.    **void PrintAtPosition(int x, int y, char symbol, ConsoleColor color)** -
Рисует в консоли символ по заданным координатам и заданному цвету.
2.    **void PrintMessageAtPosition(int x, int y, string text, ConsoleColor color)** - Печатает в консоли текст по заданным координатам и заданному цвету.
3.    **void Render(BGField bgField)** - Отрисовка игрового поля.
4.    **void SetSplash(string filename)** - Устанавливает превью-анимацию для игры.
5.    **void SetSplash(string filename, int timeout)** - Устанавливает превью-анимацию для игры с покадровой задержкой.

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

### Interface Game

Методы:
1.    **void Create(BGSurface)** - Вызывается при запуске игры.
2.    **void Run(ConsoleKey)** - Вызывается при каждом игровом такте.
3.    **void SplashScreen()** - Вызывается для установки превью-анимации игры.
4.    **void Start()** - Срабатывает при выходе из состояния PAUSE
5.    **void Pause()** - Срабатывает при входе в состояние PAUSE

### Пример SampleGame

1. Реализуйте интерфейс **Game** в вашем классе.

```C#
public class SampleGame : Game
{
    //implements
}
```

2. Реализуйте методы **Сreate(BGSurface)**, **Run(ConsoleKey)**, **SplashScreen()**, **Start()**, **Pause()**.

```C#
public class SampleGame : Game
{
    public void Create(BGSurface surface)
    {
        throw new NotImplementedException();
    }

    public void Run(ConsoleKey key)
    {
        throw new NotImplementedException();
    }

    public void SplashScreen()
    {
        throw new NotImplementedException();
    }

    public void Start()
    {
        throw new NotImplementedException();
    }

    public void Pause()
    {
        throw new NotImplementedException();
    }
}
```

3. Сохраним экземпляр класса **BGSurface**

```C#
public class SampleGame : Game
{
    
    private BGSurface surface;

    public void Create(BGSurface surface)
    {
        this.surface = surface;
    }
    
    //...
    
 }
 ```

4. Реализуем метод Run()

```C#
public class SampleGame : Game
{

    private BGSurface surface;
    
    //...

    public void Run(ConsoleKey key)
    {
        surface.Render(new BGField());
    }
    
    //...
    
 }
 ```
 
 5. Устанавливаем превью-анимацию [Пример](https://github.com/emilg1101/BrickGame/blob/master/bin/Debug/tanki.sph)
 
 
```C#
public class SampleGame : Game
{
   
    //...

    public void SplashScreen()
    {
        surface.SetSplash("sample_game.sph"); //Файл с анимацией
    }
    
    //...
    
 }
 ```
 
 6. Добавляем состояние игры.
 
  
```C#
public class SampleGame : Game
{
   
    //...
    
    private bool pause;

    public void Run()
    {
        if (pause) return;
        //...
    }
    
    //...
    
    public void Start()
    {
        pause = false;
    }
    
    public void Pause()
    {
        pause = true;
    }
 }
 ```
 Полный код
 ```C#
 namespace BrickGameEmulator
{
    public class SampleGame : Game
    {
        private BGSurface surface;

        private bool pause;
        
        public void Create(BGSurface surface)
        {
            this.surface = surface;
        }

        public void Run(ConsoleKey key)
        {
            if (pause) return;
            
            surface.Render(new BGField());
        }

        public void SplashScreen()
        {
            surface.SetSplash("sample_game.sph");
        }

        public void Start()
        {
            pause = false;
        }

        public void Pause()
        {
            pause = true;
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
