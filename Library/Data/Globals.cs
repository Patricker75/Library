namespace Library.Data
{
    public enum Gender
    {
        Male = 0,
        Female = 1,
        Other = 2
    };

    public enum MemberType
    {
        Student = 0,
        Professional = 1
    }

    public enum MemberStatus
    {
        Active = 0,
        Inactive = 1,
        Suspended = 2 // Account Balance too High
    }

    public enum Audience
    {
        Children = 0,
        Young_Adult = 1,
        Adult = 2,
        Researcher = 3
    }

    public enum RoomType
    {
        Study = 0,
        Meeting = 1,
        Piano = 2
    }

    public enum DeviceType
    {
        EReader = 0,
        Computer = 1
    }

    public enum ItemType
    {
        Book = 1,
        Device = 2
    }

    public enum Genre
    {
        Nonfiction = 0,
        Fiction = 1,
        Fantasy = 2,
        ScienceFiction = 3,
        Biography = 4
    }

    public enum JobRole
    {
        Admin = 0,
        Manager = 1,
        Librarian = 2,
        Technician = 3
    }
}
