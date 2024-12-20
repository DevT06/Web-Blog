const url = "http://localhost:5199/api";

export async function getAllCategories() {
    let response = await fetch(`${url}/category/all`);

    if (!response.ok) {
        throw new Error("An error occurred while fetching categories")
    }

    let data = await response.json();
    return data;
}

export async function getCategoryById(id) {
    let response = await fetch(`${url}/category/${id}`);

    if (!response.ok) {
        throw new Error("An error occurred while fetching categories")
    }

    let data = await response.json();
    return data;
}