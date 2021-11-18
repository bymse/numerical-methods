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
plot(x,y);
hold on
y_2 = func(x);
plot(x,y_2)
hold off

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