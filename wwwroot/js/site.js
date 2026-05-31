document.addEventListener("DOMContentLoaded", function () {

    const fab = document.getElementById("aiFab");
    const box = document.getElementById("aiChat");
    const closeBtn = document.getElementById("aiClose");
    const msgs = document.getElementById("aiMsgs");
    const input = document.getElementById("aiText");
    const send = document.getElementById("aiSend");

   

function openChat() {
    box.classList.remove("d-none");
    input.focus();
}

function closeChat() {
    box.classList.add("d-none");
}

function addBubble(text, cls) {
    const div = document.createElement("div");
    div.className = "ai-bubble " + cls;
    div.textContent = text;
    msgs.appendChild(div);
    msgs.scrollTop = msgs.scrollHeight;
}

async function doSend() {
    const text = input.value.trim();
    if (!text) return;

    input.value = "";
    addBubble(text, "ai-user");

    addBubble("Typing...", "ai-bot");
    const typing = msgs.lastChild;

    try {
        const res = await fetch("/api/ai/chat", {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({ message: text })
        });

        const data = await res.json();
        typing.remove();
        addBubble(data.reply || "Try again.", "ai-bot");
    } catch {
        typing.remove();
        addBubble("AI error. Try later.", "ai-bot");
    }
}

fab.addEventListener("click", openChat);
closeBtn.addEventListener("click", closeChat);
send.addEventListener("click", doSend);
input.addEventListener("keydown", e => {
    if (e.key === "Enter") doSend();
});

});