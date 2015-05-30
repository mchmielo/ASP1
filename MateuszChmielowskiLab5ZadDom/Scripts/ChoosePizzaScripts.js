
var finalCost = 0;
var chosenPizzas = [];
$(document).on("change", "#doughPickNumber", function ()
{;
    var thisName = $(this).attr("name");
    var tempString = thisName.split("_", 2)[1];
    var thisDoughID = parseInt(tempString);
    var parentTr = $(this).closest("tr");
    var priceTd = parentTr.find("td:nth-child(3)");
    var priceText = priceTd.find("span:nth-child(" + thisDoughID + ")").text;
    finalCost += (priceText.split(" ", 2)[0]);
    $("finalCost").text = (priceText.split(" ", 2)[0]);
});