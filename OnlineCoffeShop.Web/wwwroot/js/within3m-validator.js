// Адаптер связывает атрибуты data-val-within3m* с jQuery-валидацией.
(function ($) {
    // 1) Регистрируем сам метод проверки.
    //    value  — введённое значение поля,
    //    element — DOM-элемент input,
    //    params — { min, max } из data-атрибутов.
    $.validator.addMethod("within3m", function (value, element, params) {
        // пустое значение пропускаем — за обязательность отвечает required
        if (!value) return true;

        var date = new Date(value);
        if (isNaN(date.getTime())) return false; // не дата

        var min = new Date(params.min);
        var max = new Date(params.max);

        return date >= min && date <= max;
    });

    // 2) Адаптер: достаёт min/max из data-val-within3m-min/-max
    //    и кладёт их в params, которые получит метод выше.
    $.validator.unobtrusive.adapters.add(
        "within3m",            // имя правила (как в C#, без префикса data-val-)
        ["min", "max"],        // имена параметров (суффиксы data-val-within3m-*)
        function (options) {
            options.rules["within3m"] = {
                min: options.params.min,
                max: options.params.max
            };
            options.messages["within3m"] = options.message; // текст из data-val-within3m
        });
})(jQuery);