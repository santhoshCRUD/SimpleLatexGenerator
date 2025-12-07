function insert(text) {
    const box = document.getElementById("equationText");
    let start = box.selectionStart;
    let end = box.selectionEnd;

    box.value = box.value.substring(0, start) + text + box.value.substring(end);
    box.focus();

    updatePreview();
}

function addFraction() {
    insert("\\frac{a}{b}");
}

function addSqrt() {
    insert("\\sqrt{a}");
}

function addIntegral() {
    insert("\\int_{0}^{1} x \\, dx");
}

function addSummation() {
    insert("\\sum_{n=1}^{10} n^2");
}

function addMatrix() {
    insert("\\begin{bmatrix} a & b \\\\ c & d \\end{bmatrix}");
}

function addSubsup() {
    insert("x_{1}^{2}");
}

function updatePreview() {
    let input = document.getElementById("equationText").value;
    let preview = document.getElementById("preview");

    preview.innerHTML = `\\(${input}\\)`;

    MathJax.typesetPromise();
}

document.addEventListener("submit", () => {
    document.getElementById("finalCode").value = document.getElementById("equationText").value;
});
