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
    level:CreateObstacle("Rectangle1", 11, 15, 0)
    level:CreateObstacle("ObstacleCircle", 3, 18, 0)
    level:CreateObstacle("Rectangle3", 5, 10, 90)
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

function GetRandomValue(array)
    return array[level:GetRandom(1 ,#array)]
end

function Wave5(countAllGroups , pause, waves)
    for i = 1, countAllGroups do
    local enemyWave = GetRandomValue(waves)
    enemyWave()
    level:PauseSeconds(pause)
    end
end

function Wave5Variant_1()
    level:CreateEnemies("зеленая сфера элитный" , 1)
    level:CreateEnemies("синяя сфера рядовой" , 4)
end

function Wave5Variant_2()
    level:CreateEnemies("красный восьмиугольник элитный" , 1)
    level:CreateEnemies("синий восьмиугольник рядовой" , 4)
end

function Wave5Variant_3()
    level:CreateEnemies("красный квадрат элитный" , 1)
    level:CreateEnemies("синий квадрат рядовой" , 4)
end

function PlayWaves()
    -- 1 волна (волна восьмиугольников)
    Wave(5, 3, 2, {"синяя сфера рядовой", "синий восьмиугольник рядовой"})
    level:PauseSeconds(2)  -- Пауза между волнами
    
    -- 2 волна (волна квадратов)
    Wave(7, 4, 2, {"синяя сфера рядовой", "синий квадрат рядовой"})
    level:PauseSeconds(2)
    
    -- 3 волна (гибридная волна)
    Wave(9, 5, 2, {"синяя сфера рядовой", "синий восьмиугольник рядовой", "синий квадрат рядовой"})
    level:PauseSeconds(2)
    
    -- 4 волна (первая элита)
    Wave(11, 2, 2, {"зеленая сфера элитный", "красный восьмиугольник элитный", "красный квадрат элитный"})
    level:PauseSeconds(2)
    
    -- 5 волна (элита + рядовые)
    Wave5(13 , 2 , {Wave5Variant_1 , Wave5Variant_2 , Wave5Variant_3})
    level:PauseSeconds(2)
end

-- Создаём препятствия
CreateObstacles()
-- Сколько башен получит игрок при старте
level:SetCountStartTowersToPlayer(2)
-- Старт игры
level:StartLevel()

-- Запускаем волны
PlayWaves()
-- Ожидаем пока все враги погибнут
level:WaitDestroyAllEnemies()
-- Выводим окно конца игры
level:CompleteLevel()