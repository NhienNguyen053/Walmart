$('.owl-one').owlCarousel({

    margin:10,

    responsive:{

        0:{

            items:2

        },

        600:{

            items:3

        },

        1000:{

            items:6

        }

    }

})

$('.owl-two').owlCarousel({

    margin:10,

    responsive:{

        0:{

            items:2

        },

        600:{

            items:4

        },

        1000:{

            items:4

        }

    }

})

$('.owl-three').owlCarousel({

    margin:10,

    responsive:{

        0:{

            items:4

        },

        600:{

            items:6

        },

        1000:{

            items:8

        }

    }

})

$('.owl-four').owlCarousel({

    margin:10,

    responsive:{

        0:{

            items:2

        },

        600:{

            items:3

        },

        1000:{

            items:3

        }

    }

})
$('.owl-five').owlCarousel({

    margin:10,

    responsive:{

        0:{

            items:2

        },

        600:{

            items:3

        },

        1000:{

            items:4

        }

    }

})
const addtocart = document.getElementById("btn2");
if(!addtocart.hasAttribute('listener')){
    addtocart.setAttribute('listener', true);
    addtocart.addEventListener('click', () => {
    const productid = document.getElementById("productid").value;
    $.ajax({
        url: "/Customer/Home/AddtoCart?productid=" + productid,
        type: "GET",
        dataType: "json",
        success: function(data){
            var count = document.getElementById("items-count");
            var total = document.getElementById("total-price");
            if(data.items_count >= 10){
                count.textContent = data.items_count;
                count.style.padding = "4px 1px 0 0";
                count.style.fontSize = "10px"
            }else {
                count.style.padding = "2px 5px 2px 5px";
                count.textContent = data.items_count;
                count.style.fontSize = "12px";
            }
            if(data.total_price >= 10 && data.total_price < 100){
                total.textContent = "$" + (data.total_price).toFixed(2);
                total.style.left = "10px";
            }else if(data.total_price >= 100 && data.total_price < 1000){
                total.textContent = "$" + (data.total_price).toFixed(2);
                total.style.left = "7px";
            }else if(data.total_price >= 1000){
                total.textContent = "$" + (data.total_price).toFixed(2);
                total.style.left = "4px";
            }
            else {
                total.textContent = "$" + (data.total_price).toFixed(2);
                total.style.left = "13px";
            }
        }
    });
});
}
function minuscart(id){
    $.ajax({
            url: "/Customer/Home/MinusCart?cartid=" + id,
            type: "GET",
            dataType: "json",
            success: function(data){
                if(data.cart_Status == "Empty"){
                    document.getElementById("notempty").style.display = "none";
                    document.getElementById("empty").style.display = "flex";
                    var count = document.getElementById("items-count");
                    var total = document.getElementById("total-price");
                    count.innerText = count.innerText - 1;
                    total.innerText = "$" + (data.total).toFixed(2);
                    if(count.innerText >= 10){
                        
                        count.style.padding = "4px 1px 0 0";
                        count.style.fontSize = "10px"
                    }else {
                        count.style.padding = "2px 5px 2px 5px";
                        count.style.fontSize = "12px";
                    }
                    if(data.total >= 10 && data.total < 100){
                        total.style.left = "10px";
                    }else if(data.total >= 100 && data.total < 1000){
                        total.style.left = "7px";
                    }else if(data.total >= 1000){
                        total.style.left = "4px";
                    }
                    else {
                        total.style.left = "13px";
                    }
                }else {
                var container = document.getElementById(data.product_Id);
                var count2 = document.getElementById("total-count");
                var count3 = document.getElementById("count3");
                var total2 = document.getElementById("total2");
                var total3 = document.getElementById("total3");
                var price = document.getElementById("price_" + data.product_Id);
                var count = document.getElementById("items-count");
                var total = document.getElementById("total-price");
                var update = document.getElementById("quantity_" + data.product_Id);
                count.innerText = count.innerText - 1;
                count2.innerText = count2.innerText - 1;
                count3.innerText = "(" + count2.innerText +" items)";
                price.innerText = "$" + (data.product_Price * data.product_Quantity).toFixed(2);
                total.innerText = "$" + (data.total).toFixed(2);
                total2.innerText = "$" + (data.total).toFixed(2);
                total3.innerText = "$" + (data.total).toFixed(2);
                if(count.innerText >= 10){
                    count.style.padding = "4px 1px 0 0";
                    count.style.fontSize = "10px"
                }else {
                    count.style.padding = "2px 5px 2px 5px";
                    count.style.fontSize = "12px";
                }
                if(data.total >= 10 && data.total < 100){
                    total.style.left = "10px";
                }else if(data.total >= 100 && data.total < 1000){
                    total.style.left = "7px";
                }else if(data.total >= 1000){
                    total.style.left = "4px";
                }
                else {
                    total.style.left = "13px";
                }
                if(update.innerText == 1){
                    container.remove();
                }else{
                    update.innerText = update.innerText - 1;
                }
            }
            }
    })
}
function pluscart(id){
    $.ajax({
            url: "/Customer/Home/PlusCart?cartid=" + id,
            type: "GET",
            dataType: "json",
            success: function(data){
                var count2 = document.getElementById("total-count");
                var count3 = document.getElementById("count3");
                var total2 = document.getElementById("total2");
                var total3 = document.getElementById("total3");
                var price = document.getElementById("price_" + data.product_Id);
                var count = document.getElementById("items-count");
                var total = document.getElementById("total-price");
                var update = document.getElementById("quantity_" + data.product_Id);
                count.innerText = parseInt(count.innerText) + 1;
                count2.innerText = parseInt(count2.innerText) + 1;
                count3.innerText = "(" + count2.innerText +" items)";
                price.innerText = "$" + (data.product_Price * data.product_Quantity).toFixed(2);
                total.innerText = "$" + (data.total).toFixed(2);
                total2.innerText = "$" + (data.total).toFixed(2);
                total3.innerText = "$" + (data.total).toFixed(2);
                update.innerText = parseInt(update.innerText) + 1;
                document.getElementById("valid_"+data.product_Id).style.display = "none";
                if(count.innerText >= 10){
                    count.style.padding = "4px 1px 0 0";
                    count.style.fontSize = "10px"
                }else {
                    count.style.padding = "2px 5px 2px 5px";
                    count.style.fontSize = "12px";
                }
                if(data.total >= 10 && data.total < 100){
                    total.style.left = "10px";
                }else if(data.total >= 100 && data.total < 1000){
                    total.style.left = "7px";
                }else if(data.total >= 1000){
                    total.style.left = "4px";
                }
                else {
                    total.style.left = "13px";
                }
            }
    })
}
function removecart(id, quantity){
    $.ajax({
        url: "/Customer/Home/RemoveCart?id=" + id + "&quantity=" + quantity,
        type: "GET",
        dataType: "json",
        success: function(data){
            if(data.cart_Status == "Empty"){
                document.getElementById("notempty").style.display = "none";
                document.getElementById("empty").style.display = "flex";
                var count = document.getElementById("items-count");
                var total = document.getElementById("total-price");
                count.innerText = parseInt(count.innerText) - quantity;
                total.innerText = "$" + (data.total).toFixed(2);
                if(count.innerText >= 10){
                    count.style.padding = "4px 1px 0 0";
                    count.style.fontSize = "10px"
                }else {
                    count.style.padding = "2px 5px 2px 5px";
                    count.style.fontSize = "12px";
                }
                if(data.total >= 10 && data.total < 100){
                    total.style.left = "10px";
                }else if(data.total >= 100 && data.total < 1000){
                    total.style.left = "7px";
                }else if(data.total >= 1000){
                    total.style.left = "4px";
                }
                else {
                    total.style.left = "13px";
                }
            }else {
            var container = document.getElementById(id);
            document.getElementById("valid_"+id).style.display = "none";
            container.remove();
            var count = document.getElementById("items-count");
            var total = document.getElementById("total-price");
            var count3 = document.getElementById("count3");
            var total2 = document.getElementById("total2");
            var total3 = document.getElementById("total3");
            count.innerText = parseInt(count.innerText) - quantity;
            total.innerText = "$" + (data.total).toFixed(2);
            count3.innerText = "(" + count.innerText +" items)";
            total2.innerText = "$" + (data.total).toFixed(2);
            total3.innerText = "$" + (data.total).toFixed(2);
            if(count.innerText >= 10){
                count.style.padding = "4px 1px 0 0";
                count.style.fontSize = "10px";
            }else {
                count.style.padding = "2px 5px 2px 5px";
                count.style.fontSize = "12px";
            }
            if(data.total >= 10 && data.total < 100){
                total.style.left = "10px";
            }else if(data.total >= 100 && data.total < 1000){
                total.style.left = "7px";
            }else if(data.total >= 1000){
                total.style.left = "4px";
            }
            else {
                total.style.left = "13px";
            }
            }
        }
    });
}
function checkinput(classname){
    const inputs = document.getElementsByClassName(classname);
    const first = inputs[0].value;
    if(first.trim() == ""){
        return false;
    }else {
        return true;
    }
}
