using CarFinderUI.Library.Models;
using GridShared;
using System;

namespace CarFinderUI.BlazorApp
{
    public class ColumnCollections
    {
        public static Action<IGridColumnCollection<CarModel>> CarColumns = c =>
        {
            c.Add(o => o.Id).Titled("Id").SetWidth(100);

            c.Add(o => o.Make).Titled("Make").SetWidth(120);

            c.Add(o => o.Model).Titled("Model").SetWidth(250);

            c.Add(o => o.Year).Titled("Year");

            c.Add(o => o.HP).Titled("HP");

            c.Add(o => o.HP).Titled("Price");
        };
    }
}
