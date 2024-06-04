document.addEventListener("DOMContentLoaded", function () {
    let loader = document.querySelector(".loader__box");
    let searcher = document.getElementById("location__search");
    let search = document.getElementById("search");
    let closeSearch = document.querySelector("#search .close__icon");
    let errorBox = document.querySelector(".error__alert");
    let closeError = document.querySelector(".error__alert .close__icon");
    let searchBtn = document.getElementById("search__btn");

    searcher.addEventListener("click", function () {
        search.classList.toggle("active");
    });

    searchBtn.addEventListener("click", function () {
        // Add your search logic here (optional)
        search.classList.remove("active");
        loader.classList.add("active");
    });

    closeSearch.addEventListener("click", function () {
        // Add your search logic here (optional)
        search.classList.remove("active");
    });

    closeError.addEventListener("click", function () {
        // Add your search logic here (optional)
        errorBox.classList.remove("active");
    });
});