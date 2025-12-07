function generateInputs() {
    const rows = parseInt(document.getElementById("rows").value);
    const cols = parseInt(document.getElementById("cols").value);

    let container = document.getElementById("tableInputs");
    container.innerHTML = "";

    let html = `<table class='table table-bordered'>`;

    for (let r = 0; r < rows; r++) {
        html += "<tr>";
        for (let c = 0; c < cols; c++) {
            html += `<td>
                        <input type='text' class='form-control cell-input' 
                               data-row='${r}' data-col='${c}' placeholder='Cell ${r + 1},${c + 1}' />
                     </td>`;
        }
        html += "</tr>";
    }

    html += "</table>";

    container.innerHTML = html;
}

document.addEventListener("submit", () => {
    const cells = document.querySelectorAll(".cell-input");

    let table = {};

    cells.forEach(input => {
        let r = input.dataset.row;
        let c = input.dataset.col;

        if (!table[r]) table[r] = {};
        table[r][c] = input.value;
    });

    document.getElementById("jsonData").value = JSON.stringify(table);
});
