using Microsoft.EntityFrameworkCore.Migrations;

namespace APICatalog.Migrations
{
    public partial class seedDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO RGV01_CATEGORIES(NAME, IMAGE_URL) VALUES('Drinks','http://www.macoratti.net/Imagens/1.jpg')");
            migrationBuilder.Sql("INSERT INTO RGV01_CATEGORIES(NAME, IMAGE_URL) VALUES('Snacks','http://www.macoratti.net/Imagens/2.jpg')");
            migrationBuilder.Sql("INSERT INTO RGV01_CATEGORIES(NAME, IMAGE_URL) VALUES('Desserts','http://www.macoratti.net/Imagens/3.jpg')");

            migrationBuilder.Sql("INSERT INTO RGV01_PRODUCTS(NAME,DESCRIPTION,PRICE,IMAGE_URL,INVENTORY,CREATEDATE,CATEGORYID) " +
                                "VALUES('Coca-Cola Zero','Coke Soda with 350 ml',5.45,'http://www.macoratti.net/Imagens/coca.jpg',50,now(),(SELECT ID FROM RGV01_CATEGORIES WHERE NAME='Drinks'))");

            migrationBuilder.Sql("INSERT INTO RGV01_PRODUCTS(NAME,DESCRIPTION,PRICE,IMAGE_URL,INVENTORY,CREATEDATE,CATEGORYID) " +
                                "VALUES('Tuna Snack','Tuna snack with mayonnaise',8.50,'http://www.macoratti.net/Imagens/atum.jpg',10,now(),(SELECT ID FROM RGV01_CATEGORIES WHERE NAME='Snacks'))");

            migrationBuilder.Sql("INSERT INTO RGV01_PRODUCTS(NAME,DESCRIPTION,PRICE,IMAGE_URL,INVENTORY,CREATEDATE,CATEGORYID) " +
                                "VALUES('Pudding 100 g','Condensed milk pudding 100g',6.75,'http://www.macoratti.net/Imagens/pudim.jpg',20,now(),(SELECT ID FROM RGV01_CATEGORIES WHERE NAME='Desserts'))");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Delete from RGV01_CATEGORIES");
            migrationBuilder.Sql("Delete from RGV01_PRODUCTS");
        }
    }
}
