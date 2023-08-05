function quickSearch(input) {
    var input, filter, table, tr, td, i;
    filter = input.value.toUpperCase();
    table = document.getElementById("quick-find-list");
    tr = table.getElementsByTagName("tr");
    for (i = 0; i < tr.length; i++) {
        td = tr[i].getElementsByTagName("td")[1];
        if (td) {
            if (td.innerHTML.toUpperCase().indexOf(filter) > -1) {
                tr[i].style.display = "";
            } else {
                tr[i].style.display = "none";
            }
        }
    }
}

function table_sort() {
    const styleSheet = document.createElement('style')
    styleSheet.innerHTML = `
                            .sorter_th-inactive span {
                                visibility:hidden;
                            }
                            .sorter_th-inactive:hover span {
                                visibility:visible;
                            }
                            .sorter_th-active span {
                                visibility: visible;
                            }
                            th.sorter_th{
                                cursor: pointer
                            }
                        `
    document.head.appendChild(styleSheet)

    document.querySelectorAll('th.sorter_th').forEach(th_elem => {
        let asc = true
        const span_elem = document.createElement('span')
        span_elem.style = "font-size:0.8rem; margin-left:0.5rem"
        span_elem.innerHTML = "▼"
        th_elem.appendChild(span_elem)
        th_elem.classList.add('sorter_th-inactive')

        const index = Array.from(th_elem.parentNode.children).indexOf(th_elem)
        th_elem.addEventListener('click', (e) => {
            document.querySelectorAll('th.sorter_th').forEach(elem => {
                elem.classList.remove('sorter_th-active')
                elem.classList.add('sorter_th-inactive')
            })
            th_elem.classList.remove('sorter_th-inactive')
            th_elem.classList.add('sorter_th-active')

            if (!asc) {
                th_elem.querySelector('span').innerHTML = '▲'
            } else {
                th_elem.querySelector('span').innerHTML = '▼'
            }
            const arr = Array.from(th_elem.closest("table").querySelectorAll('tbody tr'))
            arr.sort((a, b) => {
                const a_val = a.children[index].innerText
                const b_val = b.children[index].innerText
                return (asc) ? a_val.localeCompare(b_val) : b_val.localeCompare(a_val)
            })
            arr.forEach(elem => {
                th_elem.closest("table").querySelector("tbody").appendChild(elem)
            })
            asc = !asc
        })
    })
}
