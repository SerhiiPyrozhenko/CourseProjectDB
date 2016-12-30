namespace Course_work_DB
{
    class Constr
    {
        public static string GetConnectionString()
        {
            string ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=e:\Course work DB\Course work DB\Yacht-club.mdf;Integrated Security=True";
            return ConnectionString;
        }
    }
}
