-- НАЗВАНИЯ ВРАГОВ
-- синяя сфера рядовой
-- зеленая сфера элитный
-- синий восьмиугольник рядовой
-- красный восьмиугольник элитный
-- синий квадрат рядовой
-- красный квадрат элитный
-- бесформенный синий рядовой
-- бесформенный красный элитный
-- зеленый шестиугольник рядовой
-- синий треугольник рядовой
-- фиолетовый треугольник элитный

-- НАЗВАНИЯ ПРЕПЯТСТВИЙ
-- Square
-- Rectangle1
-- Rectangle2
-- Rectangle3
-- ObstacleCircle
-- ObstacleBig

function CreateObstacles()
    -- Параметры: Имя , позиция X , позиция Y , поворот в градусах (90, 180 и тд)
    -- Нужно учитывать если был поворот то может быть смещение
    level:CreateObstacle("Rectangle1", 5, 5, 0)
    level:CreateObstacle("Square", 0, 0, 0)
end

function GetRandomValue(array)
    return array[level:GetRandom(1, #array)]
end

function Wave(countAllGroups, countInGroup, pause, namesEnemies)
    for i = 1, countAllGroups do
        local enemyName = GetRandomValue(namesEnemies)
        level:CreateEnemies(enemyName, countInGroup)
        level:PauseSeconds(pause)
    end
end

function PlayWaves()
    -- 1 волна (легкая)
    Wave(5, 3, 1.5, {"синяя сфера рядовой", "зеленый шестиугольник рядовой"})
    level:PauseSeconds(3)  -- Пауза между волнами
    
    -- 2 волна (больше врагов)
    Wave(8, 4, 1, {"синяя сфера рядовой", "зеленый шестиугольник рядовой", "синий треугольник рядовой"})
    level:PauseSeconds(4)
    
    -- 3 волна (первые элитные враги)
    Wave(6, 2, 1.2, {"зеленая сфера элитный", "синий восьмиугольник рядовой", "синий квадрат рядовой"})
    level:PauseSeconds(3)
    
    -- 4 волна (микс рядовых и элитных)
    Wave(10, 3, 0.8, {
        "синяя сфера рядовой", 
        "зеленая сфера элитный", 
        "синий квадрат рядовой", 
        "красный квадрат элитный"
    })
    level:PauseSeconds(5)
    
    -- 5 волна (тяжелые враги)
    Wave(7, 2, 1.5, {
        "красный восьмиугольник элитный", 
        "бесформенный красный элитный", 
        "фиолетовый треугольник элитный"
    })
    level:PauseSeconds(2)
    
    -- 6 волна (финальная, много сильных врагов)
    for i = 1, 20 do
        local enemies = {
            "красный восьмиугольник элитный",
            "бесформенный красный элитный",
            "фиолетовый треугольник элитный",
            "красный квадрат элитный"
        }
        level:CreateEnemies(GetRandomValue(enemies), 2)
        level:PauseSeconds(0.5 + 0.1 * i)  -- Постепенно увеличиваем паузу
    end
end

-- Создаём препятствия
CreateObstacles()
-- Сколько башен получит игрок при старте
level:SetCountStartTowersToPlayer(0)
-- Старт игры
level:StartLevel()

-- Запускаем волны
PlayWaves()
-- Ожидаем пока все враги погибнут
level:WaitDestroyAllEnemies()
-- Выводим окно конца игры
level:CompleteLevel()