namespace ItJustWorks
{
	public interface IInitialiser
	{
		public bool Initialised { get; }

		// Initialise(new object[] {0, 0.1f, true, "hi"})
		// Initialise(0, 0.1f, true, "hi")
		// params dynamically creates an array of the amount of parameters
		// we pass into the function call
		public void Initialise(params object[] _data);
	}
}