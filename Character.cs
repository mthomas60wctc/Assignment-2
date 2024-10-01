class Character{
    public UInt64 Id {get; set;}
    public String Name { get; set; } = string.Empty;
    public String Description { get; set; } = string.Empty;
    public String Species { get; set; } = String.Empty;
    public String Appearance { get; set; } = string.Empty;
    public UInt64 YearCreated { get; set; }

    public Character (UInt64 Id, String Name, String Description, String Species, String Appearance, UInt64 YearCreated){
        this.Id = Id;
        this.Name = Name;
        this.Description = Description;
        this.Species = Species;
        this.Appearance = Appearance;
        this.YearCreated = YearCreated;
    }

    public String DisplayCharacter(){
        return $"Id: {Id}\nName: {Name}\nDescription: {Description}\nFirst Appearance: {Appearance}\nSpecies: {Species}\nYear Created: {YearCreated}";
    }

    public String GenerateCSV(){
        return $"{Id},{Name},{Description},{Species},{Appearance},{YearCreated}";
    }
}