public enum EFoodType
{
	HAMBURGER = 0, 
	HOTDOG = 1, 
	COKE = 2
}

public enum EMaterialType
{
	BREAD = 1,//0x00001, 
	MEAT = 2,//0x00010, 
	KETCHUP = 4,//0x00100, 
	CABBAGE = 8,//0x01000, 
	TOMATO = 16//0x10000
}

public enum EHamburgetViewType
{
	BREAD = 1, 
	BREAD_MEAT = 3, 
	BREAD_MEAT_CABBAGE = 11,
	BREAD_MEAT_TOMATO = 19,
	BREAD_MEAT_CABBAGE_TOMATO = 27
}

public enum EHotdogViewType
{
	BREAD = 1,
	BREAD_MEAT = 3,
	BREAD_MEAT_KETCHUP = 7
}

public enum EPlateStatus
{
	LOCKED,
	EMPTY, 
	COOKING
}

public enum EHotPlateStatus
{
	EMPTY = 0, 
	COOKING = 1,
	WELLDONE = 2,
	BURN = 3
}

public enum ECokeStatus
{
	EMPTY, 
	FULL
}
