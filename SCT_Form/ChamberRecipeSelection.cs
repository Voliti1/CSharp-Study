namespace SCT_Form
{
    public class ChamberRecipeSelection
    {
        public string Module { get; set; }
        public string RecipePath { get; set; }
        public string RecipeName { get; set; }
        public string RecipePPID { get; set; }
        public int ProcessTime { get; set; }

        public ChamberRecipeSelection Clone()
        {
            return new ChamberRecipeSelection
            {
                Module = Module,
                RecipePath = RecipePath,
                RecipeName = RecipeName,
                RecipePPID = RecipePPID,
                ProcessTime = ProcessTime
            };
        }
    }
}
