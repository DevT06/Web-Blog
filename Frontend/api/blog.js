const url = "http://localhost:5199/api";

export async function getAllBlogs() {
    let response = await fetch(`${url}/blog/list`);

    if (!response.ok) {
        throw new Error("An error occurred while fetching blogs");
    }

    let data = await response.json();
    return data;
}

export async function getBlogById(id) {
    let response = await fetch(`${url}/blog/${id}`);

    if (!response.ok) {
        throw new Error("An error occurred while fetching blogs");
    }

    let data = await response.json();
    return data;
}

//later add title
export async function getBySearch(categoryId, searchTerm) {
    let response = await fetch(`${url}/blog/list?categoryId=${categoryId}&searchTerm=${searchTerm}`)

    if (!response.ok) {
        throw new Error("An error occurred while fetching categories")
    }

    let data = await response.json();
    return data;
}

export async function deleteBlogById(id) {
    let response = await fetch(`${url}/blog/delete/${id}`, {
        method: "DELETE",
        headers: {
            "Accept": "text/plain",
        }
    });

    if (!response.ok) {
        throw new Error('Failed to delete blog');
    }
}

export async function createNewBlog(blog) {
    let response = await fetch(`${url}/blog/create`, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
            "Accept": "application/json",
        },
        body: JSON.stringify(blog),
    });
    
    if (!response.ok) {
        throw new Error("An error occurred while creating the blog");
    }

    const data = await response.json();
    return data;
}

export async function updateNewBlog(id, blog) {
    let response = await fetch(`${url}/blog/update/${id}`, {
        method: "PUT",
        headers: {
            "Content-Type": "application/json",
            "Accept": "application/json",
        },
        body: JSON.stringify(blog),
    });

    if (!response.ok) {
        throw new Error("An error occurred while creating the blog");
    }

    const data = await response.json();
    return data;
}