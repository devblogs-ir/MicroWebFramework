namespace PipelineDesignPattern
{
    public  class NotFoundUrlException:Exception
    {
        public NotFoundUrlException() : base("Error 404 : This Url does not Exist!") 
        {
                
        }
    }
}
