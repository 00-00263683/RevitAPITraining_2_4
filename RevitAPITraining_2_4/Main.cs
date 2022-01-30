using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Mechanical;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitAPITraining_2_4
{
    [Transaction(TransactionMode.Manual)]
    public class Main : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;

            List<Duct> ducts = new FilteredElementCollector(doc)
                .OfClass(typeof(Duct))
                .Cast<Duct>()
                .ToList();

            int Level1 = 0;
            int Level2 = 0;

            foreach (var duct in ducts)
            {
                string level = duct.get_Parameter(BuiltInParameter.RBS_START_LEVEL_PARAM).AsValueString();
                if (level == "Level 1")
                {
                    Level1++;
                }
                else if (level == "Level 2")
                {
                    Level2++;
                }
            }

            TaskDialog.Show("Количество воздуховодов",
                $"1 этаж: {Level1}{Environment.NewLine}" +
                $"2 этаж: {Level2}");

            return Result.Succeeded;
        }
    }
}
