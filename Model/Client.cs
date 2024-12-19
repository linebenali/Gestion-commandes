public class Client
{
    public int Id { get; set; }
    public string Nom { get; set; }
    public string Adresse { get; set; }
    public string Telephone { get; set; }

    public Client(string nom, string adresse, string telephone)
    {
        Nom = nom ?? throw new ArgumentNullException(nameof(nom));
        Adresse = adresse ?? throw new ArgumentNullException(nameof(adresse));
        Telephone = telephone ?? throw new ArgumentNullException(nameof(telephone));
    }
      public Client()
    {
    }
}
