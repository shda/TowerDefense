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
    level:CreateObstacle("Rectangle1", 7, 18, 90)
    level:CreateObstacle("Rectangle2", 3, 16, 0)
    level:CreateObstacle("Rectangle2", 11, 16, 0)
    level:CreateObstacle("Rectangle3", 7, 14, 90)
    level:CreateObstacle("Square", 8, 10, 0)
    level:CreateObstacle("Square", 8, 22, 0)
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
    level:CreateEnemies("синяя сфера рядовой" , 4)
end

function Wave2Variant_2()
    level:CreateEnemies("зеленый шестиугольник рядовой" , 4)
end

function Wave2Variant_3()
    level:CreateEnemies("синяя сфера рядовой" , 2)
    level:CreateEnemies("зеленый шестиугольник рядовой" , 2)
end

function Wave4(countAllGroups , pause, waves)
    for i = 1, countAllGroups do
    local enemyWave = GetRandomValue(waves)
    enemyWave()
    level:PauseSeconds(pause)
    end
end

function Wave4Variant_1()
    level:CreateEnemies("зеленая сфера элитный" , 1)
    level:CreateEnemies("синяя сфера рядовой" , 3)
end

function Wave4Variant_2()
    level:CreateEnemies("бесформенный красный элитный" , 1)
    level:CreateEnemies("бесформенный синий рядовой" , 3)
end

function Wave4Variant_3()
    level:CreateEnemies("зеленый шестиугольник рядовой" , 6)
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
    level:CreateEnemies("синяя сфера рядовой" , 2)
    level:CreateEnemies("зеленый шестиугольник рядовой" , 2)
end

function Wave5Variant_2()
    level:CreateEnemies("бесформенный красный элитный" , 1)
    level:CreateEnemies("бесформенный синий рядовой" , 2)
    level:CreateEnemies("зеленый шестиугольник рядовой" , 2)
end

function Wave5Variant_3()
    level:CreateEnemies("зеленая сфера элитный" , 1)
    level:CreateEnemies("бесформенный красный элитный" , 1)
    level:CreateEnemies("зеленый шестиугольник рядовой" , 2)
end

function PlayWaves()
    -- 1 волна (волна шестиугольников)
    Wave(5, 3, 2, {"синяя сфера рядовой", "зеленый шестиугольник рядовой"})
    level:PauseSeconds(2)  -- Пауза между волнами
    
    -- 2 волна (гибридная волна)
    Wave2(7 , 2 , {Wave2Variant_1 , Wave2Variant_2 , Wave2Variant_3})
    level:PauseSeconds(2)
    
    -- 3 волна (волна бесформенных)
    Wave(9, 5, 2, {"синяя сфера рядовой", "бесформенный синий рядовой"})
    level:PauseSeconds(2)
    
    -- 4 волна (элита и рядовые I)
    Wave4(11 , 2 , {Wave4Variant_1 , Wave4Variant_2 , Wave4Variant_3})
    level:PauseSeconds(2)
    
    -- 5 волна (элита и рядовые II)
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