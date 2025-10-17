using cw15.DTOs;
using cw15.Infrastructure.Database;
using cw15.Services;
using cw15.Tools;

using var db = new AppDbContext();
var _service = new ProductService();

while (true)
{
    Console.Clear();
    Console.WriteLine(" Product Search");
    Console.WriteLine("------------------");

    var filter = new ProductSearchDto();

    Console.Write("Product name (optional): ");
    filter.Name = Console.ReadLine();

    Console.Write("Min price (optional): ");
    if (decimal.TryParse(Console.ReadLine(), out decimal min)) filter.MinPrice = min;

    Console.Write("Max price (optional): ");
    if (decimal.TryParse(Console.ReadLine(), out decimal max)) filter.MaxPrice = max;

    Console.Write("Color (optional): ");
    filter.Color = Console.ReadLine();

    Console.Write("Brand (optional): ");
    filter.Brand = Console.ReadLine();

    Console.Write("Category ID (optional): ");
    if (int.TryParse(Console.ReadLine(), out int catId)) filter.CategoryId = catId;

    Console.Write("Category name (optional): ");
    filter.CategoryName = Console.ReadLine();

    // Sorting
    string wSort;
    do
    {
        Console.Write("Do you want to sort? (y/n): ");
        wSort = Console.ReadLine()?.ToLower();

        if (wSort == "y")
        {
            var sortBy = new ProductSortDto();
            Console.Write("Sort by (price / name / stock): ");
            sortBy.SortBy = Console.ReadLine()?.ToLower();
            Console.Write("Descending? (y/n): ");
            sortBy.IsDescending = Console.ReadLine()?.ToLower() == "y";
            filter.Sort.Add(sortBy);
        }

    } while (wSort == "y");

    filter.PageSize = 3;
    int currentPage = 1;

    while (true)
    {
        filter.PageNumber = currentPage;

        var (results, totalCount) = _service.SearchProduct(filter);

        Console.Clear();
        Console.WriteLine($"Page {currentPage} of {Math.Ceiling((double)totalCount / filter.PageSize)}\n");

        if (results.Count == 0)
            Console.WriteLine("No products found.");
        else
            ConsolePainter.WriteTable(results);

        Console.WriteLine("\n[n] next | [p] previous | [r] new search | [q] quit");
        var key = Console.ReadKey(true).KeyChar;

        if (key == 'n' && currentPage * filter.PageSize < totalCount)
            currentPage++;
        else if (key == 'p' && currentPage > 1)
            currentPage--;
        else if (key == 'r')
            break;
        else if (key == 'q')
            return;
    }
}
