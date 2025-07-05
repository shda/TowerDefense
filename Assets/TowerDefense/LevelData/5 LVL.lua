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
    level:CreateObstacle("Rectangle1", 3, 15, 90)
    level:CreateObstacle("Rectangle3", 11, 16, 90)
    level:CreateObstacle("Rectangle3", 3, 11, 90)
    level:CreateObstacle("Rectangle1", 11, 10, 90)
    level:CreateObstacle("Rectangle2", 10, 13, 0)
    level:CreateObstacle("Rectangle2", 4, 18, 0)
    level:CreateObstacle("Rectangle2", 4, 8, 0)
    level:CreateObstacle("ObstacleBig", 7, 22, 0)
    level:CreateObstacle("Square", 3, 22, 0)
    level:CreateObstacle("Square", 13, 22, 0)
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

function Wave3(countAllGroups , pause, waves)
    for i = 1, countAllGroups do
    local enemyWave = GetRandomValue(waves)
    enemyWave()
    level:PauseSeconds(pause)
    end
end

function Wave3Variant_1()
    level:CreateEnemies("зеленая сфера элитный" , 1)
    level:CreateEnemies("синяя сфера рядовой" , 2)
end

function Wave3Variant_2()
    level:CreateEnemies("красный восьмиугольник элитный" , 1)
    level:CreateEnemies("синий восьмиугольник рядовой" , 2)
end

function Wave3Variant_3()
    level:CreateEnemies("красный квадрат элитный" , 1)
    level:CreateEnemies("синий квадрат рядовой" , 2)
end

function Wave3Variant_4()
    level:CreateEnemies("бесформенный красный элитный" , 1)
    level:CreateEnemies("бесформенный синий рядовой" , 2)
end

function Wave3Variant_5()
    level:CreateEnemies("фиолетовый треугольник элитный" , 1)
end

function Wave3Variant_6()
    level:CreateEnemies("синий треугольник рядовой" , 2)
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

function Wave5Variant_4()
    level:CreateEnemies("бесформенный красный элитный" , 1)
    level:CreateEnemies("бесформенный синий рядовой" , 4)
end

function Wave5Variant_5()
    level:CreateEnemies("фиолетовый треугольник элитный" , 1)
    level:CreateEnemies("синий треугольник рядовой" , 1)
end

function PlayWaves()
    -- 1 волна (элита)
    Wave(5, 1, 2, {"зеленая сфера элитный", "красный восьмиугольник элитный", "красный квадрат элитный", "бесформенный красный элитный", "синий треугольник рядовой"})
    level:PauseSeconds(2)  -- Пауза между волнами
    
    -- 2 волна (рядовые)
    Wave(7, 4, 2, {"синяя сфера рядовой", "синий квадрат рядовой", "синий восьмиугольник рядовой", "зеленый шестиугольник рядовой", "бесформенный синий рядовой"})
    level:PauseSeconds(2)
    
    -- 3 волна (смешанная волна)
    Wave3(9 , 2 , {Wave3Variant_1 , Wave3Variant_2 , Wave3Variant_3 , Wave3Variant_4 , Wave3Variant_5 , Wave3Variant_6})
    level:PauseSeconds(2)
    
    -- 4 волна (рядовые)
    Wave(10, 6, 2, {"синяя сфера рядовой", "синий квадрат рядовой", "синий восьмиугольник рядовой", "зеленый шестиугольник рядовой", "бесформенный синий рядовой"})
    level:PauseSeconds(2)
    
    -- 5 волна (смешанная волна)
    Wave5(11 , 2 , {Wave5Variant_1 , Wave5Variant_2 , Wave5Variant_3 , Wave5Variant_4 , Wave3Variant_5})
    level:PauseSeconds(2)

    -- 6 волна (рядовые)
    Wave(12, 8, 2, {"синяя сфера рядовой", "синий квадрат рядовой", "синий восьмиугольник рядовой", "зеленый шестиугольник рядовой", "бесформенный синий рядовой"})
    level:PauseSeconds(2)

    -- 7 волна (элита)
    Wave(13, 3, 2, {"зеленая сфера элитный", "красный восьмиугольник элитный", "красный квадрат элитный", "бесформенный красный элитный", "синий треугольник рядовой"})
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