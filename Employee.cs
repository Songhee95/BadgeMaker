namespace CatWorx.BadgeMaker
{
    class Employee
    {
        //access modifier : private vs public
        public string FirstName;
        public string LastName;
        public int Id;
        public string PhotoUrl;
        // Add a constructor that accepts a first name and sets the FirstName property.
        public Employee(string firstName, string lastName, int id, string photoUrl)
        {
            FirstName = firstName;
            LastName = lastName;
            Id = id;
            PhotoUrl = photoUrl;
        }
        public string GetName()
        {
            return FirstName + " " + LastName;
        }
        public int GetId()
        {
            return Id;
        }
        public string GetPhotoUrl()
        {
            return PhotoUrl;
        }
        public string GetCompanyName()
        {
            return "Cat Worx";
        }
    }
}

