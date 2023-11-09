using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Populate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //here we are just populating the database so I have something to display before implement the add apis
            migrationBuilder.Sql(@"
                INSERT INTO Items(ItemId,Name) VALUES
                    (1,'Onion'),
                    (2,'Jerjer'),
                    (3,'Tomato');
            ");

            migrationBuilder.Sql(@"
                INSERT INTO Packages(PackageId,Name) VALUES
                    (1,'Plastic Bag'),
                    (2,'Carton Box'),
                    (3,'Metal Container');
            ");

            migrationBuilder.Sql(@"
                INSERT INTO Branches(BranchId,Name) VALUES
                    (1,'Headquarters'),
                    (2,'Eastern Branch'),
                    (3,'Western Branch');
            ");

            migrationBuilder.Sql(@"
                INSERT INTO InventoryInHeaders VALUES
                    (1,1,'2023-07-12','Some Random Reference','No Remarks'),
                    (2,3,'2023-12-07','Another Random Reference','All Remarks');
            ");

            migrationBuilder.Sql(@"
                INSERT INTO InventoryInDetails VALUES
                    (1,1,123,2,2,'2323232323','4545454545','2024-05-12',12.4,19.2),
                    (2,2,123,3,3,'67676767676','9090909090','2030-01-11',78.9,0.12);
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //Clearing the tables
            migrationBuilder.Sql(@"
                DELETE FROM InventoryInDetails;
            ");

            migrationBuilder.Sql(@"
                DELETE FROM InventoryInHeaders;
            ");

            migrationBuilder.Sql(@"
                DELETE FROM Branches;
            ");


            migrationBuilder.Sql(@"
                DELETE FROM Packages;
            ");

            migrationBuilder.Sql(@"
                DELETE FROM Items;
            ");
        }
    }
}
