import { getAllBlogs } from "./api/blog.js";
import { getBySearch } from "./api/blog.js";
import { deleteBlogById } from "./api/blog.js";
import { getBlogById } from "./api/blog.js";
import { createNewBlog } from "./api/blog.js";
import { updateNewBlog } from "./api/blog.js";

import { getAllCategories } from "./api/category.js";

//later implement automatic reloading after time period (1 min) 

let searchTerm = "";
let categoryId = 0;


let quillEditor;

const toolbarOptions = [
    [{ 'header': [1, 2, 3, 4, 5, false] }],
    [{ 'size': ['huge', 'large', false, 'small'] }],  // custom dropdown

    ['bold', 'italic', 'underline', 'strike'],        // toggled buttons
    ['blockquote', 'code-block'],
    ['link', 'image', /*'video',*/ 'formula'],

    [{ 'header': 1 }, { 'header': 2 }],               // custom button values
    [{ 'list': 'ordered' }, { 'list': 'bullet' }, { 'list': 'check' }],
    //[{ 'script': 'sub'}, { 'script': 'super' }],      // superscript/subscript
    //[{ 'indent': '-1'}, { 'indent': '+1' }],          // outdent/indent
    [{ 'direction': 'rtl' }],                         // text direction //remove later


    [{ 'color': [] }, { 'background': [] }],          // dropdown with defaults from theme
    //[{ 'font': [] }],
    [{ 'align': [] }],

    ['clean']                                         // remove formatting button
];

async function loadCategoryData() {
    let categories = await getAllCategories();

    let selectField = $('#selectField')
    //selectField.unwrap();
    categories.forEach(category => {
        selectField.append(`<option value="${category.id}">${category.name}</option>`)
    });

}

async function searchBlog() {
    let searchField = $(`#searchField`);
    searchTerm = searchField.val();
    loadBlogData();
}

async function changeCategory() {
    let selectField = $('#selectField');
    categoryId = selectField.val();
    loadBlogData();
}

function changeCategoryEditorMode() {
    let selectField = $('#selectField');
    categoryId = selectField.val();
}

async function loadPageData() {
    loadBlogData();
    loadCategoryData();
}

async function deleteBlog(blogId/*, blogTitle*/) {
    if (confirm(`Delete this Blog? Id: ${blogId} `)) {
        deleteBlogById(blogId)
        let removedEntry = $(`#blog_${blogId}`);
        removedEntry.detach();
    };
}

async function detailViewBlog(blogId) {
    window.location.href = `./blog.html?id=${blogId}`;
}

async function loadDetailSite() {
    let url = location.href;
    let blogId = url.split("=")[1];
    let blog;
    try {
        blog = await getBlogById(blogId.valueOf())
    } catch (e) {
        let detailViewTitle = $('#detailBlogView');
        detailViewTitle.append("Not found")
        let createdAt = $(`#createdAt`);
        let editedAt = $(`#editedAt`);
        let blogTitle = $('#blogTitle');
        createdAt.detach();
        editedAt.detach();
        blogTitle.text("404, Blog Not found :/")
        return
    }
    const options = {
        modules: {
            toolbar: false,
        },
        theme: 'snow'
    };
    const quill = new Quill('#editor', options);
    quill.enable(false);
    let detailViewTitle = $('#detailBlogView');
    let blogTitle = $('#blogTitle');
    let blogAuthor = $('#blogAuthor');
    let createdAt = $(`#createdAt`);
    let editedAt = $(`#editedAt`);
    let categoryName = $(`#categoryName`);
    let buttonContainer = $(`#buttonContainer`)
    buttonContainer.append(`<button class="btn btn-success align-self-right" onclick="goToEditPage(${blog.id})">Edit <i class="bi bi-pen"></i></button>`)
    categoryName.text(`${blog.category.name}`)
    detailViewTitle.append(`${blog.id}`);
    blogTitle.text(`${blog.title}`);
    blogAuthor.text(`${blog.author}`);
    createdAt.append(`${blog.createdAt.replace("T", " ").slice(0, 16)}`)
    blog.editedAt === null ? editedAt.detach() : editedAt.append(`${blog.editedAt.replace("T", " ").slice(0, 16)}`);
    quill.setContents(JSON.parse(blog.text))
    quillEditor = quill;
}

function loadCreatePage() {
    loadCategoryData();

    const quill = new Quill('#editor', {
        modules: {
            toolbar: toolbarOptions
        },
        theme: 'snow'
    });
    quillEditor = quill;
}



async function goToEditPage(blogId) {
    location.href = `./editBlog.html?id=${blogId}`;
}

async function loadBlogEditPage() {

    let url = location.href;
    let blogId = url.split("=")[1];
    let blog;
    try {
        blog = await getBlogById(blogId.valueOf())
    } catch (e) {
        let blogEdit = $('#blogEdit');
        blogEdit.append("Not found")
        let createdAt = $(`#createdAt`);
        let editedAt = $(`#editedAt`);
        let allLabels = $(`label`);
        let allInputs = $(`input`);
        let selectField = $(`#selectField`);
        let titleInputContainer = $(`#titleInputContainer`);
        let buttonContainer = $(`#buttonContainer`);
        buttonContainer.empty();
        buttonContainer.append(`<button class="btn btn-success" form="editBlogForm" onclick="goToCreateBlog()">Create new Blog <i class="bi-box-arrow-up-right"></i></button>`)
        titleInputContainer.append(`<h1 class="text-center">404, Blog not found :/</h1>`);
        selectField.detach();
        allLabels.detach();
        allInputs.detach();
        createdAt.detach();
        editedAt.detach();
        return;
    }
    loadCategoryData();

    const quill = new Quill('#editor', {
        modules: {
            toolbar: toolbarOptions
        },
        theme: 'snow'
    });
    quillEditor = quill;

    let blogEdit = $('#blogEdit');
    let blogTitleInput = $('#titleInput');
    let blogAuthor = $('#authorInput');
    let createdAt = $(`#createdAt`);
    let editedAt = $(`#editedAt`);
    let category = $(`#currentCategory`)
    createdAt.append(`${blog.createdAt.replace("T", " ").slice(0, 16)}`)
    blog.editedAt === null ? editedAt.detach() : editedAt.append(`${blog.editedAt.replace("T", " ").slice(0, 16)}`);
    blogTitleInput.val(`${blog.title}`)
    blogAuthor.val(`${blog.author}`)
    category.val(`${blog.category.id}`)
    category.text(`${blog.category.name}`)
    let buttonContainer = $(`#buttonContainer`);
    blogEdit.append(`${blog.id}`)
    quill.setContents(JSON.parse(blog.text))
}

async function updateBlog() {
    let url = location.href;
    let blogId = url.split("=")[1];
    let titleInput = $(`#titleInput`);
    let authorInput = $(`#authorInput`);
    let selectField = $(`#selectField`);

    let blogTitle = titleInput.val();
    let blogAuthor = authorInput.val();
    let blogCategory = selectField.val();

    if (blogTitle.trim() !== "" && blogAuthor.trim() !== "" && blogCategory !== "") {

        let newBlog = {
            title: blogTitle,
            text: JSON.stringify(quillEditor.getContents().ops),
            author: blogAuthor,
            lastChangedAt: new Date().toISOString(),
            categoryId: blogCategory
        }

        await updateNewBlog(blogId, newBlog);

        window.location.replace(`/blog.html?id=${blogId}`)
    }
}

$("form").submit(function () { return false; });

async function goBackMain() {
    window.location.href = `./listBlog.html`
    loadPageData();

}

async function goBackMainSpecificBlog() {
    let url = location.href;
    let blogId = url.split("=")[1];
    window.location.href = `./listBlog.html#b${blogId}`
    loadPageData();
}


async function goBackDetail() {
    let url = location.href;
    let blogId = url.split("=")[1];
    window.location.replace(`/blog.html?id=${blogId}`)
    await loadDetailSite();
}

function goToCreateBlog() {
    window.location.href = `./createBlog.html`
}


async function createBlog() {
    let titleInput = $(`#titleInput`);
    let authorInput = $(`#authorInput`);
    let selectField = $(`#selectField`);

    let blogTitle = titleInput.val();
    let blogAuthor = authorInput.val();
    let blogCategory = selectField.val();

    if (blogTitle.trim() !== "" && blogAuthor.trim() !== "" && blogCategory !== "") {

        let newBlog = {
            title: blogTitle,
            text: JSON.stringify(quillEditor.getContents().ops),
            author: blogAuthor,
            lastChangedAt: new Date().toISOString(),
            categoryId: blogCategory
        }

        let blog = await createNewBlog(newBlog);

        detailViewBlog(blog.id);
    }
}

window.goBackMainSpecificBlog = goBackMainSpecificBlog;
window.updateBlog = updateBlog;
window.changeCategoryEditorMode = changeCategoryEditorMode;
window.createBlog = createBlog;

window.loadCreatePage = loadCreatePage;
window.goToCreateBlog = goToCreateBlog;
window.goBackDetail = goBackDetail;
window.goToEditPage = goToEditPage;
window.goBackMain = goBackMain;
window.loadDetailSite = loadDetailSite;
window.loadBlogEditPage = loadBlogEditPage;
window.detailViewBlog = detailViewBlog;
window.searchBlog = searchBlog;
window.changeCategory = changeCategory;
window.loadBlogData = loadBlogData;
window.loadPageData = loadPageData;
window.deleteBlog = deleteBlog;


async function loadBlogData() {
    let blogs = await getBySearch(categoryId, searchTerm);

    let tableContent = $('#tableContent');
    tableContent.empty();

    blogs.forEach(blog => {
        tableContent.append(`<tr style="height: 85px; " class="" id="b${blog.id}"> <!--<td>${blog.id}</td>--> <td class="fs-5">${blog.title}</td> <td class="fs-5">${blog.author}</td> <td class="fs-5">${blog.category.name}</td> <td class="fs-5">${blog.createdAt.replace("T", " ").slice(0, 16)/*.replaceAll("-", ".")*/}</td> <td class="fs-5">${blog.editedAt !== null ? blog.editedAt.replace("T", " ").slice(0, 16) : ""}</td> <td><button onclick="deleteBlog(${blog.id})" class="btn btn-danger"><i class="bi bi-trash"></i></button></td> <td><button onclick="detailViewBlog(${blog.id})" class="btn btn-primary">Detail <i class="bi bi-arrow-right-circle"></i>
</button></td> </tr>`)
    });

    let url = location.href;
    let blogId = url.split("#")[1];
    scrollToElementWithMargin(blogId, 70)
}

function scrollToElementWithMargin(elementId, margin) {
    const element = document.getElementById(elementId);
    if (element) {
        const elementPosition = element.getBoundingClientRect().top + window.pageYOffset;
        const offsetPosition = elementPosition - margin;

        window.scrollTo({
            top: offsetPosition,
            behavior: 'instant'
        });
    }
}

// Example starter JavaScript for disabling form submissions if there are invalid fields
(() => {
    'use strict'

    // Fetch all the forms we want to apply custom Bootstrap validation styles to
    const forms = document.querySelectorAll('.needs-validation')

    // Loop over them and prevent submission
    Array.from(forms).forEach(form => {
        form.addEventListener('submit', event => {
            if (!form.checkValidity()) {
                event.preventDefault()
                event.stopPropagation()
            }

            form.classList.add('was-validated')
        }, false)
    })
})()