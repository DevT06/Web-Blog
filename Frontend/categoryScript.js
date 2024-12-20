import { getCategoryById } from "./api/category.js";

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