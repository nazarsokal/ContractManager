document.addEventListener("DOMContentLoaded", function () {

    // EDIT BUTTON
    document.querySelectorAll(".edit-btn").forEach(btn => {
        btn.addEventListener("click", function () {
            const row = btn.closest("tr");

            row.querySelectorAll(".editable").forEach(cell => {
                const value = cell.innerText.trim();
                const field = cell.dataset.field;
                let input;

                if (field === "IsMarried") {
                    input = document.createElement("select");
                    input.innerHTML = `
                        <option value="true">Yes</option>
                        <option value="false">No</option>`;
                    input.value = value === "Yes" ? "true" : "false";
                } else if (field === "DateOfBirth") {
                    input = document.createElement("input");
                    input.type = "date";
                    input.value = value;
                } else {
                    input = document.createElement("input");
                    input.type = "text";
                    input.value = value;
                }

                input.classList.add("form-control");
                cell.dataset.original = value;
                cell.innerHTML = "";
                cell.appendChild(input);
            });

            toggleButtons(row, true);
        });
    });

    // CANCEL BUTTON
    document.querySelectorAll(".cancel-btn").forEach(btn => {
        btn.addEventListener("click", function () {
            const row = btn.closest("tr");

            row.querySelectorAll(".editable").forEach(cell => {
                cell.innerText = cell.dataset.original;
            });

            toggleButtons(row, false);
        });
    });

    // SAVE BUTTON
    document.querySelectorAll(".save-btn").forEach(btn => {
        btn.addEventListener("click", function () {
            const row = btn.closest("tr");
            const id = row.dataset.id;

            const data = {};
            row.querySelectorAll(".editable").forEach(cell => {
                const field = cell.dataset.field;
                const input = cell.querySelector("input, select");

                if (field === "IsMarried") {
                    data[field] = input.value === "true";
                    cell.innerText = input.value === "true" ? "Yes" : "No";
                } else {
                    data[field] = input.value;
                    cell.innerText = input.value;
                }
            });

            const queryString = new URLSearchParams(data).toString();

            fetch(`/update/${id}?${queryString}`, {
                method: 'PUT'
            })
                .then(res => {
                    if (!res.ok) console.error('Update failed');
                })
                .catch(err => console.error(err));

            toggleButtons(row, false);
        });
    });

    // DELETE BUTTON
    document.querySelectorAll(".delete-btn").forEach(btn => {
        btn.addEventListener("click", function () {
            const row = btn.closest("tr");
            const id = row.dataset.id;

            if (!confirm("Are you sure you want to delete this contact?")) return;

            fetch(`/delete/${id}`, {
                method: 'DELETE'
            })
                .then(res => {
                    if (res.ok) {
                        row.remove(); // remove row from table
                    } else {
                        console.error("Delete failed");
                    }
                })
                .catch(err => console.error(err));
        });
    });

    // Toggle visibility of buttons
    function toggleButtons(row, editing) {
        row.querySelector(".edit-btn").classList.toggle("d-none", editing);
        row.querySelector(".save-btn").classList.toggle("d-none", !editing);
        row.querySelector(".cancel-btn").classList.toggle("d-none", !editing);
    }
});
