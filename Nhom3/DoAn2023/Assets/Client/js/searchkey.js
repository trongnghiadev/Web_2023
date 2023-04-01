// Typing search

const dataArray = [
    "Thực tập sinh PHP",
    "Thực tập sinh .NET",
    "Thực tập sinh Javascript",
    "Thực tập sinh ReactJS",
    "Thực tập sinh AngularJS",
    "Thực tập sinh NodeJS",
    "Thực tập sinh VueJS",
    "Thực tập sinh Java",
    "Thực tập sinh Android",
    "Thực tập sinh iOS",
    "Thực tập sinh React Native",
    "Thực tập sinh CEO",
    "Thiết kế website",
    "IT Helpdesk",
    "Unity Developer",
    "Tester"
];

_(".search__input").onkeyup = (e) => {
    let userData = e.target.value;
    let emptyArray = [];
    if (userData) {
        emptyArray = dataArray.filter((data) => {
            return data.toLocaleLowerCase().startsWith(userData.toLocaleLowerCase());
        });
        emptyArray = emptyArray.map((data) => {
            return data = `<li>${data}</li>`;
        });
        _(".search__autocom--box").classList.add('active');
        showSuggestions(emptyArray);

        let allList = __(".search__autocom--box li");
        for (let i = 0; i < allList.length; i++) {
            allList[i].onclick = () => {
                let selectUserData = allList[i].textContent;
                _('#keyword').value = selectUserData;
                _(".search__autocom--box").classList.remove('active');
            }
        }
    } else {
        _(".search__autocom--box").innerHTML = null;
        _(".search__autocom--box").classList.remove('active');
    }
}

function showSuggestions(list) {
    let listData;
    if (!list.length) {
        listData = null;
        _(".search__autocom--box").classList.remove('active');
    } else {
        listData = list.join("");
    }
    _(".search__autocom--box").innerHTML = listData;
}

