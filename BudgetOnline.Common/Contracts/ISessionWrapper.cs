namespace BudgetOnline.Common.Contracts
{
	public interface ISessionWrapper
	{
		bool IsAvailable { get; }
		void Put(string key, object obj);
		T Get<T>(string key);
		bool Get<T>(string key, out T result);
		bool Exists(string key);
		void Remove(string key);
		void Abandon();
	}
}
