export const scrollToBottom = () => {
    const messages = document.getElementsByClassName("message");
    if (messages && messages.length != 0) {
        messages[messages.length - 1].scrollIntoView({
            behavior: "smooth",
            block: "end",
            inline: "nearest"
        });
    }
}
