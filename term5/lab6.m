func = @(x) x*2.*log(x+2);
sum = 0;
for n = 0:3
    Q = calculate_L(n);
    disp(Q);
    c = calculate_c(func, @(x) cast(subs(Q, x), 'double'));
    sum = sum + c.*Q;
end

x = -1:1/10:1;
y = subs(sum, x);
y_ideal = func(x);

hold on
task1(func);
plot(x,y);
plot(x, y_ideal);
hold off

legend('МНК', 'Лежандр', 'Эталон');

function c = calculate_c(func, Q)
    numerator = integral(@(x) func(x).*Q(x), -1, 1);
    denominator = integral(@(x) (Q(x)).^2, -1, 1);
    c = numerator / denominator;
end

function L = calculate_L(n)
    syms x
    func = (x^2-1)^n;
    L = (1/(factorial(n)*2^n)) .* diff(func, n);
end