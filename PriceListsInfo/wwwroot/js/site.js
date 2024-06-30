document.addEventListener('DOMContentLoaded', function () {
    const addColumnBtn = document.getElementById('add-column-btn');
    const columnsContainer = document.getElementById('columns');
    const form = document.getElementById('price-list-form');

    addColumnBtn.addEventListener('click', function () {
        const columnCount = columnsContainer.children.length;

        const newColumnHtml =
            `<div class="row mb-3 column-item">
                <div class="col-5">
                    <div class="input-group">
                        <span class="input-group-text">Название колонки</span>
                        <input name="Columns[${columnCount}].ColumnName" type="text" class="form-control" required />
                    </div>
                </div>
                <div class="col-5">
                    <div class="input-group">
                        <label class="input-group-text" for="columnType_${columnCount}">Тип колонки</label>
                        <select name="Columns[${columnCount}].ColumnType" class="form-select" id="columnType_${columnCount}" required>
                            <option value="" selected>Выберите тип колонки...</option>
                            <option value="Number">Число</option>
                            <option value="SingleLineText">Однострочный текст</option>
                            <option value="MultiLineText">Многострочный текст</option>
                        </select>
                    </div>
                </div>
                <div class="col-2">
                    <div class="text-center p-2 btn btn-danger remove-column-btn">Удалить</div>
                </div>
            </div>`;

        columnsContainer.insertAdjacentHTML('beforeend', newColumnHtml);
        addRemoveColumnListeners();
    });

    function addRemoveColumnListeners() {
        const removeColumnBtns = document.querySelectorAll('.remove-column-btn');
        removeColumnBtns.forEach(button => {
            button.removeEventListener('click', handleRemoveColumn);
            button.addEventListener('click', handleRemoveColumn);
        });
    }

    function handleRemoveColumn(event) {
        const columnItem = event.target.closest('.column-item');
        columnItem.remove();
    }

    addRemoveColumnListeners();

    form.addEventListener('submit', function (event) {
        if (!form.checkValidity()) {
            event.preventDefault();
            event.stopPropagation();
            alert('Please fill out all required fields.');
        }
    });
});

document.addEventListener('DOMContentLoaded', function () {
    const dateInput = document.getElementById('creation-date');

    if (dateInput.value === '01.01.0001 0:00:00' || dateInput.value === '') {
        dateInput.value = '';
    } else {
        const date = new Date(dateInput.value);
        if (!isNaN(date)) {
            dateInput.value = date.toLocaleDateString('ru-RU');
        }
    }

    dateInput.addEventListener('focus', function (event) {
        event.target.type = 'date';
    });

    dateInput.addEventListener('blur', function (event) {
        if (!event.target.value) {
            event.target.type = 'text';
        }
    });
});

document.addEventListener('DOMContentLoaded', function () {
    const form = document.querySelector('form');

    form.addEventListener('submit', function (event) {
        let isValid = true;

        form.querySelectorAll('input, textarea').forEach(function (input) {
            if (!input.checkValidity()) {
                isValid = false;
                input.classList.add('is-invalid');
            } else {
                input.classList.remove('is-invalid');
            }
        });

        if (!isValid) {
            event.preventDefault();
            event.stopPropagation();
        }
    });
});

document.addEventListener('DOMContentLoaded', function () {
    const numberInputs = document.querySelectorAll('input[type="number"]');

    numberInputs.forEach(function (input) {
        input.addEventListener('input', function () {
            this.value = this.value.replace(/[^0-9]/g, '');
        });
    });
});

document.addEventListener('DOMContentLoaded', function () {
    const table = document.getElementById('priceListTable');
    const headers = table.querySelectorAll('th');

    headers.forEach(header => {
        header.addEventListener('click', () => {
            const columnIndex = Array.from(headers).indexOf(header);
            const rows = Array.from(table.tBodies[0].rows);
            const currentSortOrder = header.getAttribute('data-sort-order');
            let sortedRows;

            if (header.dataset.column.toLowerCase().includes('дата')) {
                sortedRows = rows.sort((a, b) => {
                    const dateA = new Date(a.cells[columnIndex].innerText.split('.').reverse().join('-'));
                    const dateB = new Date(b.cells[columnIndex].innerText.split('.').reverse().join('-'));
                    return currentSortOrder === 'asc' ? dateA - dateB : dateB - dateA;
                });
            } else if (!isNaN(rows[0].cells[columnIndex].innerText)) {
                sortedRows = rows.sort((a, b) => {
                    const numA = parseFloat(a.cells[columnIndex].innerText);
                    const numB = parseFloat(b.cells[columnIndex].innerText);
                    return currentSortOrder === 'asc' ? numA - numB : numB - numA;
                });
            } else {
                sortedRows = rows.sort((a, b) => {
                    const textA = a.cells[columnIndex].innerText;
                    const textB = b.cells[columnIndex].innerText;
                    return currentSortOrder === 'asc' ? textA.localeCompare(textB) : textB.localeCompare(textA);
                });
            }

            // Очистка существующих строк и добавление отсортированных строк
            while (table.tBodies[0].firstChild) {
                table.tBodies[0].removeChild(table.tBodies[0].firstChild);
            }
            table.tBodies[0].append(...sortedRows);

            // Переключение порядка сортировки
            header.setAttribute('data-sort-order', currentSortOrder === 'asc' ? 'desc' : 'asc');
        });
    });
});

$.get('/PriceLists/GetPriceLists', function (data) {
    console.log('Price lists loaded:', data);

    data.forEach(function (item) {
        $('#priceListSelect').append(new Option(item.name, item.id));
    });
}).fail(function (jqXHR, textStatus, errorThrown) {
    console.error('Error loading price lists:', textStatus, errorThrown);
});

$(document).ready(function () {
    $('#priceListForm').on('submit', function (event) {
        if ($(document.activeElement).hasClass('no-validation')) {
            return;
        }
        let isValid = true;

        // Очистка предыдущих ошибок
        $('.text-danger').text('');

        // Валидация поля "Название"
        const name = $('#name').val().trim();
        if (!name) {
            $('#nameError').text('Название обязательно.');
            isValid = false;
        }

        // Валидация поля "Дата создания"
        const date = $('#date').val().trim();
        if (!date) {
            $('#dateError').text('Дата создания обязательна.');
            isValid = false;
        }

        // Валидация колонок
        $('.column-item').each(function (index, element) {
            const columnName = $(element).find('input[name^=Columns][name$=ColumnName]').val().trim();
            const columnType = $(element).find('input[name^=Columns][name$=ColumnType]').val().trim();

            if (!columnName) {
                $(element).find('.text-danger').first().text('Название колонки обязательно.');
                isValid = false;
            }

            if (!columnType) {
                $(element).find('.text-danger').last().text('Тип колонки обязателен.');
                isValid = false;
            }
        });

        if (!isValid) {
            event.preventDefault();
        }
    });
});