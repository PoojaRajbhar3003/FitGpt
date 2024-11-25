namespace FitGpt.Models.ResponseModels
{
    public class RepositoryResponseModel<T>
    {
        public bool Success { get; set; }
        public T? Data { get; set; }
    }
}
