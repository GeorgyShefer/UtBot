 class Program
{
    private static void Main(string[] args)
    {
        Method();
      
    }
    async static void Method()
    {
      await Task.Run(() =>
      {
          while ((File.Exists("C:\\Users\\пк\\Desktop\\svo.docx")))
          {

          }
      });


    }
}