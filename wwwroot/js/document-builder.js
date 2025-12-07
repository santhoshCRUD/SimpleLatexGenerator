let sectionIndex = 0;

function addSection() {
    const container = document.getElementById("sectionsContainer");

    const html = `
        <div class='card p-3 mb-3'>
            <label class='form-label fw-bold'>Section Title</label>
            <input type='text' class='form-control section-title' data-id='${sectionIndex}'/>

            <label class='form-label mt-2 fw-bold'>Content</label>
            <textarea class='form-control section-content' rows='3' data-id='${sectionIndex}'></textarea>
        </div>
    `;

    container.insertAdjacentHTML("beforeend", html);
    sectionIndex++;
}

document.addEventListener("submit", () => {
    const titles = document.querySelectorAll(".section-title");
    const contents = document.querySelectorAll(".section-content");

    let list = [];

    titles.forEach((el, i) => {
        list.push({
            title: el.value,
            content: contents[i].value
        });
    });

    document.getElementById("jsonSections").value = JSON.stringify(list);
});
