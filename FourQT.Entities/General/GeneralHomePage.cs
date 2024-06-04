namespace FourQT.Entities.General
{
    public class GeneralHomePage
    {
        public GenHomePageRow1? row1 { get; set; } = new GenHomePageRow1();
        public GenHomePageRow2? row2 { get; set; } = new GenHomePageRow2();
        public GenHomePageRow3? row3 { get; set; } = new GenHomePageRow3();
        public GenHomePageRow4? row4 { get; set; } = new GenHomePageRow4();
        public GenHomePageRow5? row5 { get; set; } = new GenHomePageRow5();
        public GenHomePageRow6? row6 { get; set; } = new GenHomePageRow6();
        public GenHomePageRow7? row7 { get; set; } = new GenHomePageRow7();
        public HomepageModuleControl? showModules { get; set; } = new HomepageModuleControl();
    }

    public class Header
    {
        public string? headerText { get; set; }
        public string? headerFont { get; set; }
        public string? headerFontSize { get; set; }
        public string? headerColor { get; set; }
    }

    public class Media
    {
        public string? media { get; set; }
        public string? type { get; set; }
    }

    public class Link
    {
        public string? link { get; set; }
    }

    public class LinkMedia 
    {
        public string? media { get; set; }
        public string? type { get; set; }
        public string? link { get; set; }
    }

    public class GenHomePageRow1
    {
        public string? headerLogo { get; set; }
    }

    public class GenHomePageRow2 : GenHomePageRow2_1
    {
        public List<Media>? mediaList { get; set; } = new List<Media>();
    }

    public class GenHomePageRow3
    {
        public string? headerText { get; set; }
        public string? headerFont { get; set; }
        public string? headerSize { get; set; }
        public string? headerColor { get; set; }
        public string? projectName { get; set; }
        public string? projectLocation { get; set; }
        public string? projectCost { get; set; }
        public string? rating { get; set; }
        public string? reraNo { get; set; }
        public string? configuration { get; set; }
        public string? sizes { get; set; }
        public string? landArea { get; set; }
        public string? location { get; set; }
        public string? projectDetails { get; set; }
        public string? downloadBrochureLink { get; set; }
        public List<LinkMedia>? mediaList { get; set; } = new List<LinkMedia>();
    }

    public class GenHomePageRow4
    {
        public string? headerText { get; set; }
        public string? headerFont { get; set; }
        public string? headerSize { get; set; }
        public string? headerColor { get; set; }
        public string? buttonText { get; set; }
        public string? buttonFont { get; set; }
        public string? buttonFontSize { get; set; }
        public string? buttonFontColor { get; set; }
        public string? buttonBackgroundColor { get; set; }
        public string? link { get; set; }
    }

    public class GenHomePageRow5
    {
        public Media? video { get; set; } = new Media();
    }

    public class GenHomePageRow6
    {
        public string? headerText { get; set; }
        public string? headerFont { get; set; }
        public string? headerSize { get; set; }
        public string? headerColor { get; set; }
        public List<Media>? mediaList { get; set; } = new List<Media>();
    }

    public class GenHomePageRow7
    {
        public List<LinkMedia>? mediaList { get; set; } = new List<LinkMedia>();
    }

    public class GenHomePageRow2_1
    {
        public string? textLine1 { get; set; }
        public string? textLine2 { get; set; }
        public string? textLine3 { get; set; }
        public string? textLine4 { get; set; }
        public string? textLine5 { get; set; }
        public string? textLine6 { get; set; }
        public string? textLine7 { get; set; }
        public string? visitLink { get; set; }
    }

    public class GenHomePageText
    {
        public string? text { get; set; }
        public string? font { get; set; }
        public string? size { get; set; }
        public string? color { get; set; }
    }

    public class MultipleAppKeys {
        public string? projectKey { get; set; }
        public string? projectLogo { get; set; }
        public string? projectName1 { get; set; }
        public string? projectName2 { get; set; }
        public int rank { get; set; }
    }

    public class HomepageModuleControl { 
        public Boolean showCustomerPortal { get; set; }
        public Boolean showEmployee { get; set; }
        public Boolean showChannelPartner { get; set; }

    }
}
