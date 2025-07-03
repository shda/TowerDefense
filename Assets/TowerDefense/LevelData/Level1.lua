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
-- ObsticleCircle
-- ObsticleBig

math.randomseed(os.time())
function GetRandomValue(array)
    return array[level:GetRandom(1 ,#array)]
end

function Wave(countAllGroups , countInGroup , pause, namesEnemies)
	for i = 1, countAllGroups do
		local enemyName = GetRandomValue(namesEnemies)
		level:CreateEnemies(enemyName , countInGroup)
		level:PauseSeconds(pause)
	end
end

function Wave5(countAllGroups , pause, wavesFunc)
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

time = 100/33

pause = 2.0
for i = 1, 33 do
	level:CreateEnemies("синяя сфера рядовой" , 1)
	level:PauseSeconds(time)
end

level:CreateEnemies("синий квадрат рядовой" , 1)
level:PauseSeconds(0.5)
level:CreateEnemies("синий квадрат рядовой" , 2)
level:PauseSeconds(0.5)
--сколько групп , сколько всего в одной группе , пауза между группами, массив имён врагов
--Wave(5 , 3 , pause , {"синяя сфера рядовой", "зеленый шестиугольник рядовой"} )
--Wave(7 , 4 , pause , {"синяя сфера рядовой", "синий квадрат рядовой"} )
--Wave(9 , 5 , pause , {"синяя сфера рядовой", "синий восьмиугольник рядовой" , "синий квадрат рядовой" } )
--Wave(11 , 2 , pause , {"зеленая сфера элитный", "красный восьмиугольник элитный" , "красный квадрат элитный" } )

--сколько групп , пауза между группами, фуекции групп
Wave5(13 , pause , {Wave5Variant_1 , Wave5Variant_2 , Wave5Variant_3})