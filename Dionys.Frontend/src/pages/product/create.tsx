import React from "react";
import { Breadcrumbs } from "@material-ui/core";
import { History } from "history";
import { Layout, Link, ProductEdit } from "../../components";
import { ServiceProvider } from "../../services";
import { Product } from "../../models";

interface Props {
    readonly history: History;
}

interface State {
    readonly product: Product;
}

class Create extends React.Component<Props, State> {
    protected readonly service = ServiceProvider.productService;

    public constructor(props) {
        super(props);

        this.state = {
            product: null
        };

        this.onModelChange = this.onModelChange.bind(this);
        this.onSubmit = this.onSubmit.bind(this);
    }

    public async componentDidMount() {
        const product: Product = {
            id: null,
            name: "",
            description: "",
            protein: 0,
            fat: 0,
            carbs: 0,
            calories: 0
        };

        this.setState({ product });
    }

    protected onModelChange(product: Product) {
        this.setState({ product });
    }

    protected async onSubmit() {
        const data = { ...this.state.product };
        delete data.id;

        const product = await this.service.create(data);
        this.props.history.push(`/products/${product.id}`);
    }

    public render() {
        const product = this.state.product;

        return (
            <Layout title={product && product.name ? product.name : "Create product"}>
                <Breadcrumbs className="breadcrumbs" aria-label="Breadcrumb">
                    <Link to="/">Home</Link>
                    <Link to="/products">Product list</Link>
                </Breadcrumbs>

                <ProductEdit product={product} onModelChange={this.onModelChange} onSubmit={this.onSubmit} />
            </Layout>
        );
    }
}

export default Create;
