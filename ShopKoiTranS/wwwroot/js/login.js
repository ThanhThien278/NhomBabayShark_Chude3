<script>
    document.addEventListener("DOMContentLoaded", function () {
    const passwordInput = document.querySelector('input[name="Password"]');

    if (passwordInput) {
        passwordInput.addEventListener("input", function () {
            const strengthLabel = document.querySelector(".password-strength");
            const strength = checkPasswordStrength(passwordInput.value);
            if (strengthLabel) {
                strengthLabel.textContent = strength;
                strengthLabel.style.color = getColor(strength);
            }
        });
    }

    function checkPasswordStrength(password) {
        if (password.length < 6) return "Yếu";
    if (password.match(/[a-z]/) && password.match(/[0-9]/) && password.match(/[@$!%*?&]/)) {
            return "Mạnh";
        }
    return "Trung bình";
    }

    function getColor(strength) {
        switch (strength) {
            case "Yếu":
    return "#e74c3c";
    case "Trung bình":
    return "#f1c40f";
    case "Mạnh":
    return "#2ecc71";
        }
    }
});
</script>
