const BuoiChieuSelect = document.getElementById("MaRap");
let RoomId = BuoiChieuSelect.value;

/*BuoiChieuSelect.addEventListener("change", (e) => {
    RoomId = e.target.value;
});*/

function attachMaRap() {

    BuoiChieuSelect.addEventListener('change', function(event) {
        event.preventDefault();
        RoomId = event.target.value;
        $.ajax({
            url: "ChonPhong",
            method: "Get",
            data:{ ma: RoomId },
            success: function (data) {
                $("div.content").html(data);
            }
        });
});
}

attachMaRap();