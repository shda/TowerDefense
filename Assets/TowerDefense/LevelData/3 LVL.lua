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
    level:CreateObstacle("Rectangle1", 3, 18, 90)
    level:CreateObstacle("Rectangle3", 11, 18, 90)
    level:CreateObstacle("Rectangle3", 3, 14, 90)
    level:CreateObstacle("Rectangle1", 11, 14, 90)
    level:CreateObstacle("Square", 8, 11, 0)
    level:CreateObstacle("Square", 8, 21, 0)
    level:CreateObstacle("Rectangle1", 7, 16, 90)
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

function Wave2(countAllGroups , pause, waves)
    for i = 1, countAllGroups do
    local enemyWave = GetRandomValue(waves)
    enemyWave()
    level:PauseSeconds(pause)
    end
end

function Wave2Variant_1()
    level:CreateEnemies("синяя сфера рядовой" , 2)
    level:CreateEnemies("зеленый шестиугольник рядовой" , 2)
end

function Wave2Variant_2()
    level:CreateEnemies("синий восьмиугольник рядовой" , 2)
    level:CreateEnemies("синий квадрат рядовой" , 2)
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
    level:CreateEnemies("зеленый шестиугольник рядовой" , 2)
end

function Wave3Variant_2()
    level:CreateEnemies("красный восьмиугольник элитный" , 1)
    level:CreateEnemies("зеленый шестиугольник рядовой" , 2)
end

function Wave3Variant_3()
    level:CreateEnemies("красный квадрат элитный" , 1)
    level:CreateEnemies("зеленый шестиугольник рядовой" , 2)
end

function Wave3Variant_4()
    level:CreateEnemies("бесформенный красный элитный" , 1)
    level:CreateEnemies("зеленый шестиугольник рядовой" , 2)
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
    level:CreateEnemies("зеленый шестиугольник рядовой" , 4)
end

function Wave5Variant_2()
    level:CreateEnemies("красный восьмиугольник элитный" , 1)
    level:CreateEnemies("зеленый шестиугольник рядовой" , 4)
end

function Wave5Variant_3()
    level:CreateEnemies("красный квадрат элитный" , 1)
    level:CreateEnemies("зеленый шестиугольник рядовой" , 4)
end

function Wave5Variant_4()
    level:CreateEnemies("бесформенный красный элитный" , 1)
    level:CreateEnemies("зеленый шестиугольник рядовой" , 4)
end

function Wave6(countAllGroups , pause, waves)
    for i = 1, countAllGroups do
    local enemyWave = GetRandomValue(waves)
    enemyWave()
    level:PauseSeconds(pause)
    end
end

function Wave6Variant_1()
    level:CreateEnemies("зеленая сфера элитный" , 2)
    level:CreateEnemies("зеленый шестиугольник рядовой" , 2)
end

function Wave6Variant_2()
    level:CreateEnemies("красный восьмиугольник элитный" , 2)
    level:CreateEnemies("зеленый шестиугольник рядовой" , 2)
end

function Wave6Variant_3()
    level:CreateEnemies("красный квадрат элитный" , 2)
    level:CreateEnemies("зеленый шестиугольник рядовой" , 2)
end

function Wave6Variant_4()
    level:CreateEnemies("бесформенный красный элитный" , 2)
    level:CreateEnemies("зеленый шестиугольник рядовой" , 2)
end

function PlayWaves()
    -- 1 волна (рядовые враги)
    Wave(5, 3, 2, {"синяя сфера рядовой", "синий квадрат рядовой", "синий восьмиугольник рядовой", "зеленый шестиугольник рядовой"})
    level:PauseSeconds(2)  -- Пауза между волнами
    
    -- 2 волна (смешанные рядовые враги)
    Wave2(7 , 2 , {Wave2Variant_1 , Wave2Variant_2})
    level:PauseSeconds(2)
    
    -- 3 волна (элита + рядовые I)
    Wave3(9 , 2 , {Wave3Variant_1 , Wave3Variant_2 , Wave3Variant_3 , Wave3Variant_4})
    level:PauseSeconds(2)
    
    -- 4 волна (много рядовых)
    Wave(10, 6, 2, {"синяя сфера рядовой", "синий квадрат рядовой", "синий восьмиугольник рядовой", "зеленый шестиугольник рядовой"})
    level:PauseSeconds(2)  -- Пауза между волнами
    
    -- 5 волна (элита + рядовые II)
    Wave5(11 , 2 , {Wave5Variant_1 , Wave5Variant_2 , Wave5Variant_3 , Wave5Variant_4})
    level:PauseSeconds(2)

    -- 6 волна (элита + рядовые III)
    Wave5(12 , 2 , {Wave5Variant_1 , Wave5Variant_2 , Wave5Variant_3 , Wave5Variant_4})
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