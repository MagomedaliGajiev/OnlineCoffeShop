(function () {
    const modal = document.getElementById('confirm-modal');
    if (!modal) return;

    const textEl = modal.querySelector('[data-confirm-text]');
    const okBtn = modal.querySelector('[data-confirm-ok]');
    const cancelBtn = modal.querySelector('[data-confirm-cancel]');
    let pendingForm = null;   // форма, которую отправим после подтверждения

    // Перехватываем отправку ЛЮБОЙ формы с классом .js-confirm-delete.
    // Делегирование на document — работает для всех таких форм на странице.
    document.addEventListener('submit', function (e) {
        const form = e.target.closest('.js-confirm-delete');
        if (!form) return;

        e.preventDefault();                 // останавливаем обычную отправку
        pendingForm = form;
        textEl.textContent = form.dataset.confirmMessage || 'Удалить запись?';
        openModal();
    });

    // «Удалить» — отправляем сохранённую форму.
    // form.submit() НЕ вызывает событие 'submit' повторно, поэтому зацикливания нет.
    okBtn.addEventListener('click', function () {
        if (pendingForm) pendingForm.submit();
    });

    // «Отмена», клик по фону, Esc — закрываем без удаления.
    cancelBtn.addEventListener('click', closeModal);
    modal.addEventListener('click', function (e) {
        if (e.target === modal) closeModal();
    });
    document.addEventListener('keydown', function (e) {
        if (e.key === 'Escape') closeModal();
    });

    function openModal() {
        modal.classList.add('is-open');
        modal.setAttribute('aria-hidden', 'false');
    }
    function closeModal() {
        modal.classList.remove('is-open');
        modal.setAttribute('aria-hidden', 'true');
        pendingForm = null;
    }
})();