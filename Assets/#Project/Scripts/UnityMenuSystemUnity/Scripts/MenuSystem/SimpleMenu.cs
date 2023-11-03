/// <summary>
/// A base menu class that implements parameterless Show and Hide methods
/// </summary>
public abstract class SimpleMenu<T> : Menu<T> where T : SimpleMenu<T>
{
	public static Menu Show()
	{
		return Open();
	}

	public static Menu ShowAsFirstMenuInStack()
	{
		return OpenFirst();
	}

	public static void Hide()
	{
		Close();
	}
}
