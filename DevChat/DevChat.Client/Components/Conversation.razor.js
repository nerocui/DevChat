export const scrollToBottom = () => {
    const messages = document.getElementsByClassName("message-list-item");
    if (messages && messages.length != 0) {
        messages[messages.length - 1].scrollIntoView({
            behavior: "smooth",
            block: "end",
            inline: "nearest"
        });
    }
}

export const scrollToNewBottom = () => {
    const messages = document.getElementsByClassName("message-new-bottom");
    if (messages && messages.length != 0) {
        messages[messages.length - 1].scrollIntoView({
            behavior: "smooth",
            block: "end",
            inline: "nearest"
        });
    }
}

let editorInstance = {
    initialized: false,
    htmlEditor: undefined,
    jsEditor: undefined,
    cssEditor: undefined,
};

export const initializeEditor = () => {
    if (editorInstance.initialized) {
        return;
    }
    const htmlTextArea = document.getElementById("code-mirror-editor-html");
    const jsTextArea = document.getElementById("code-mirror-editor-js");
    const cssTextArea = document.getElementById("code-mirror-editor-css");

    editorInstance.htmlEditor = CodeMirror.fromTextArea(htmlTextArea, {
        lineNumbers: true,
        mode: { name: "xml", htmlMode: true },
    });
    editorInstance.htmlEditor.on("keydown", (cm, e) => {
        DotNet.invokeMethodAsync("DevChat.Client", "IndicateTyping");
    });
    editorInstance.htmlEditor.on("keyup", function (cm, e) {
        // Check if the key combination is Ctrl-Enter
        if (e.ctrlKey && e.keyCode == 13) {
            // Prevent the default behavior
            e.preventDefault();
            // Call a C# method named SayHello from the MyAssembly assembly
            DotNet.invokeMethodAsync("DevChat.Client", "OnSendMessage");
        }
    });


    editorInstance.jsEditor = CodeMirror.fromTextArea(jsTextArea, {
        lineNumbers: true,
        mode: "javascript"
    });
    editorInstance.jsEditor.on("keydown", (cm, e) => {
        DotNet.invokeMethodAsync("DevChat.Client", "IndicateTyping");
    });

    editorInstance.cssEditor = CodeMirror.fromTextArea(cssTextArea, {
        lineNumbers: true,
        mode: "css"
    });
    editorInstance.cssEditor.on("keydown", (cm, e) => {
        DotNet.invokeMethodAsync("DevChat.Client", "IndicateTyping");
    });

    editorInstance.initialized = true;
}

export const getHtmlString = () => {
    if (!editorInstance.initialized) {
        return "";
    }

    return editorInstance.htmlEditor.getValue();
}

export const resetHtmlString = () => {
    editorInstance.htmlEditor?.setValue("");
}

export const getJsString = () => {
    if (!editorInstance.initialized) {
        return "";
    }

    return editorInstance.jsEditor.getValue();
}

export const resetJsString = () => {
    editorInstance.jsEditor?.setValue("");
}

export const getCssString = () => {
    if (!editorInstance.initialized) {
        return "";
    }

    return editorInstance.cssEditor.getValue();
}

export const resetCssString = () => {
    editorInstance.cssEditor?.setValue("");
}

// A function that checks if a string is html
export const isHtml = (str) => {
    // Create a temporary element
    let temp = document.createElement("div");
    // Set the innerHTML of the element to the string
    temp.innerHTML = str;
    // If the element has any child nodes, it means the string is html
    return temp.childNodes.length > 0;
}
