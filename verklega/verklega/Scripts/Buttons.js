$('#ViewRequest').click(function ()
{
    location.href = '@Url.Action("ViewRequest", "ViewRequest", new { id = ViewRequest})';
}
);